using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public struct PersonaStr
{
	public int _life;
	public int _hunger;
	public int _thirst;
	public int _bodyTemperature;
}

[System.Serializable]
public struct RessourcesStr
{
	public int _meats;
	public int _waters;
	public int _plants;
	public int _stones;
	public int _woods;
	public int _skins;
}


//public class Wood
//{
//	public string name = "wood";
//	public int value;
//}

//public struct Data
//{
//	public Data(int intValue, string strValue)
//	{
//		IntegerData = intValue;
//		StringData = strValue;
//	}

//	public int IntegerData { get; set; }
//	public string StringData { get; set; }
//}

//class MyClass
//{
//	List<string> _testRess;
//}

//public enum MyEnum
//{
//	bois,
//	eaux,
//	skins
//}

public class GameManager : MonoBehaviour
{
	[Header("Persona References")]
	public List<RectTransform> _positionsOfPersonas;
	public List<PersonaData> _listOfPersonaData;
	public List<GameObject> _listOfPersonasGO;
	public List<Persona> _listOfPersona;

	[Header("Event References")]
	public List<EventData> _eventsList;

	public List<Deposit> _listOfDeposit = new List<Deposit>();

	[Header("Game Infos")]
	public int _currentDay;
	public EventData _currentEvent;
	public RessourcesStr _mainRessources;
	public bool _gameOver;

	CanvasManager _canvasManager;


	private void Awake()
	{
		_canvasManager = FindObjectOfType<CanvasManager>();
		_gameOver = false;
		_currentDay = 1;
	}

	private void Start()
	{
		StartCoroutine(GameLoop());
	}

	IEnumerator GameLoop()
	{	
		Debug.Log("Dans GameLoop");

		// Zone Tuto
		while (_canvasManager._panelTutorialGO.activeSelf == true)
			yield return null;


		StartCoroutine(InitializePersonas());
		_canvasManager.InitializeRessourcesBar();
		_canvasManager.UpdateRessourcesBar(_mainRessources);


		// GameLoop
		while (_gameOver == false)
			yield return StartCoroutine(NewDay());

		Debug.Log("Fin du jeu");
	}


	#region Personas Functions
	IEnumerator InitializePersonas()
	{
		if (_listOfPersonaData.Count > 0)
		{
			_listOfPersonasGO = new List<GameObject>();
			_listOfPersona = new List<Persona>();
			Canvas canvas = FindObjectOfType<Canvas>();

			for (int i = 0; i < _listOfPersonaData.Count; i++)
			{
				GameObject newPersona = new GameObject();
				newPersona.transform.SetParent(canvas.transform.Find("Spawn"));
				newPersona.name = _listOfPersonaData[i]._namePersona;

				RectTransform rectTransform = newPersona.AddComponent<RectTransform>();
				rectTransform.position = _positionsOfPersonas[i].position;
				rectTransform.sizeDelta = new Vector2(200, 200);

				Image spritePersona = newPersona.AddComponent<Image>();
				spritePersona.sprite = _listOfPersonaData[i]._spriteOfPersona;
				spritePersona.preserveAspect = true;

				Persona personaInstance = newPersona.AddComponent<Persona>();
				personaInstance._nameOfPersona = _listOfPersonaData[i]._namePersona;
				personaInstance._personaStats = _listOfPersonaData[i]._personaStr;
				personaInstance._listOfActions = _listOfPersonaData[i]._listOfActions;
				_listOfPersona.Add(personaInstance);

				Button personaButton = newPersona.AddComponent<Button>();
				personaButton.onClick.AddListener(personaInstance._OpenPanelActionEvent);
			
				_listOfPersonasGO.Add(newPersona);
			}
		}
		else
			Debug.Log("Aucun Persona définis");

		yield return null;
	}

	public void EatingAllPersonas()
	{
		foreach (Persona persona in _listOfPersona)
		{
			persona.EatingPersona();
		}
	}
	public void DrinkingAllPersonas()
	{
		foreach (Persona persona in _listOfPersona)
		{
			persona.DrinkingPersona();
		}
	}
	public void HealingAllPersonas()
	{
		foreach (Persona persona in _listOfPersona)
		{
			persona.HealingPersona();
		}
	}
	#endregion


	#region Next Day Functions
	IEnumerator NewDay()
	{
		Debug.Log("Dans NewDay");
		int tempDay = _currentDay;

		yield return StartCoroutine(NewEvent());

		while (tempDay == _currentDay)
			yield return null;
	}
	IEnumerator NextDay()
	{
		// Fermerture des panels
		_canvasManager.OpenClosePanelAction(false);
		_canvasManager.OpenClosePanelNextDay(false);

		// Application des besoins journaliers des personas
		foreach (Persona item in _listOfPersona)
		{
			item.DailyNeeds();
			item.ActionPersona();
		}

		foreach (Deposit item in _listOfDeposit)
		{
			item.UpdateDeposit();
		}

		// Fondu au noir
		yield return StartCoroutine(_canvasManager.FadeToBlack());

		// Changement du jour
		_currentDay++;
	}
	public void NextDayButton()
	{
		StartCoroutine(NextDay());
	}
	#endregion


	#region Event Functions
	IEnumerator NewEvent()
	{
		RandomEvent();
		_canvasManager._panelEventGO.SetActive(true);

		while (_canvasManager._panelEventGO.activeSelf == true)
			yield return null;
	}
	public void ApplyEvent()
	{
		Debug.Log("Apply Event");
		UpDateRessources(_currentEvent._ressourcesStr);
		_canvasManager.UpdateRessourcesBar(_mainRessources);

		_canvasManager.OpenClosePanelEvent();
		_canvasManager.OpenClosePanelNextDay(true);
	}
	void RandomEvent()
	{
		_currentEvent = _eventsList[Random.Range(0, _eventsList.Count)];
		string title = "Jour " + _currentDay.ToString() + ": " + _currentEvent._eventName;
		_canvasManager.UpdateEventText(title, _currentEvent._eventDescription);
	}
	#endregion


	#region Ressources functions
	void UpDateRessources(RessourcesStr newRessources)
	{
		_mainRessources._meats = Mathf.Clamp(_mainRessources._meats + newRessources._meats, 0, 100);
		_mainRessources._plants = Mathf.Clamp(_mainRessources._plants + newRessources._plants, 0, 100);
		_mainRessources._skins = Mathf.Clamp(_mainRessources._skins + newRessources._skins, 0, 100);
		_mainRessources._stones = Mathf.Clamp(_mainRessources._stones + newRessources._stones, 0, 100);
		_mainRessources._waters = Mathf.Clamp(_mainRessources._waters + newRessources._waters, 0, 100);
		_mainRessources._woods = Mathf.Clamp(_mainRessources._woods + newRessources._woods, 0, 100);
	}
	#endregion
}

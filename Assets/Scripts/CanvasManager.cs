using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	[Header("Panel References")]
	public GameObject _panelActionGO;
	public GameObject _panelEventGO;
	public GameObject _panelTutorialGO;
	public GameObject _panelNextDayGO;
	public GameObject _fadeToBlack;

	[Header("Ressources Bar")]
	public GameObject _ressourcesBar;
	public int _fontSizeRB;
	public Color _fontColorRB;

	[Header("Fade Options")]
	[Range(0, 10)] public float _fadeDuration;

	Text _eventTitle;
	Text _eventDescription;
	List<Text> _ressourcesText;


	public Vector3 _eventPanelPositionOpen;
	public Vector3 _eventPanelPositionClose;
	public RectTransform _eventTransform;
	public Vector3 _velocity;
	public float duration;
	public bool _panelEventIsVisible;

	public PanelAction _panelActionMain;
	public Dropdown _dropdownAction;


	private void Awake()
	{
		_panelActionMain = _panelActionGO.GetComponent<PanelAction>();
		_dropdownAction = _panelActionGO.GetComponentInChildren<Dropdown>();

		_panelTutorialGO.SetActive(true);

		_ressourcesBar.SetActive(false);
		_panelActionGO.SetActive(false);
		_panelNextDayGO.SetActive(false);
		_panelEventGO.SetActive(false);
		_eventTitle = _panelEventGO.transform.Find("EventTitle").GetComponent<Text>();
		_eventDescription = _panelEventGO.transform.Find("EventDescription").GetComponent<Text>();
	}


	#region Panel Tutorial
	public void ClosePanelTutorial()
	{
		_panelTutorialGO.SetActive(false);
	}
	#endregion


	#region Panel Action
	public void OpenClosePanelAction(bool isActive)
	{
		_panelActionGO.SetActive(isActive);
	}
	#endregion


	#region Panel Event
	public void OpenClosePanelEvent()
	{
		StartCoroutine(PanelEventTranslation());
	}
	IEnumerator PanelEventTranslation()
	{
		RectTransform rectTransform = _panelEventGO.GetComponent<RectTransform>();
		float time=0;
		float tRatio;

		if(_panelEventIsVisible == false)
		{
			Debug.Log(false);
			while (time < duration)
			{
				tRatio =  time/duration;
				rectTransform.anchoredPosition = Vector2.Lerp(_eventPanelPositionOpen, _eventPanelPositionClose,tRatio);
				time += Time.deltaTime;

				yield return null;
			}
			rectTransform.anchoredPosition = _eventPanelPositionClose;
			_panelEventIsVisible = true;
		}
		else if (_panelEventIsVisible == true)
		{
			Debug.Log(true);
			while (time < duration)
			{
				tRatio = time / duration;
				rectTransform.anchoredPosition = Vector2.Lerp(_eventPanelPositionClose, _eventPanelPositionOpen, tRatio);
				time += Time.deltaTime;

				yield return null;
			}
			rectTransform.anchoredPosition = _eventPanelPositionOpen;
			_panelEventIsVisible = false;
		}
	}
	public void UpdateEventText(string title, string description)
	{
		_eventTitle.text = title;
		_eventDescription.text = description;
	}
	#endregion


	#region Panel Next Day
	public void OpenClosePanelNextDay(bool isActive)
	{
		_panelNextDayGO.SetActive(isActive);
	}
	#endregion


	#region Ressources Bar
	public void InitializeRessourcesBar()
	{
		_ressourcesBar.SetActive(true);
		_ressourcesText = new List<Text>();

		for (int i = 0; i < 6; i++)
		{
			GameObject newRessourceText = new GameObject();
			newRessourceText.transform.SetParent(transform.Find("ResourcesBar"));

			_ressourcesText.Add(newRessourceText.AddComponent<Text>());
			_ressourcesText[i].font = Font.CreateDynamicFontFromOSFont("Arial", 12);
			_ressourcesText[i].fontSize = _fontSizeRB;
			_ressourcesText[i].alignment = TextAnchor.MiddleCenter;
			_ressourcesText[i].color = _fontColorRB;

		}
	}
	public void UpdateRessourcesBar(RessourcesStr mainRessources)
	{
		//_ressourcesText[0].text = "Meats: " + mainRessources._meats.ToString();
		//_ressourcesText[1].text = "Plants: " + mainRessources._plants.ToString();
		//_ressourcesText[2].text = "Skins: " + mainRessources._skins.ToString();
		//_ressourcesText[3].text = "Stones: " + mainRessources._stones.ToString();
		//_ressourcesText[4].text = "Waters: " + mainRessources._waters.ToString();
		//_ressourcesText[5].text = "Woods: " + mainRessources._woods.ToString();
	}
	#endregion


	#region FadeToBlack
	public IEnumerator FadeToBlack()
	{
		_fadeToBlack.SetActive(true);

		Image spriteToFade = _fadeToBlack.GetComponent<Image>();
		spriteToFade.CrossFadeAlpha(0,0, true);
		spriteToFade.CrossFadeAlpha(1, _fadeDuration * 0.5f, true);
		yield return new WaitForSeconds(_fadeDuration * 0.5f);
		spriteToFade.CrossFadeAlpha(0, _fadeDuration * 0.5f, true);
		yield return new WaitForSeconds(_fadeDuration * 0.5f);

		_fadeToBlack.SetActive(false);
	}
	#endregion
}

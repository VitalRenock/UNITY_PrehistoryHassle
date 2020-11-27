using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Persona : MonoBehaviour
{
	public string _nameOfPersona;
	public PersonaStr _personaStats;
	public List<ActionData> _listOfActions;
	public ActionData _currentAction;

	GameManager _gameManager;
	CanvasManager _canvasManager;
	Deposit _depositManager;

	public UnityAction _OpenPanelActionEvent;
	public UnityAction _eatingPersona;
	public UnityAction _drinkingPersona;
	public UnityAction _healingPersona;

	private void Awake()
	{
		_gameManager = FindObjectOfType<GameManager>();
		_canvasManager = FindObjectOfType<CanvasManager>();
		_depositManager = FindObjectOfType<Deposit>();

		_OpenPanelActionEvent += OpenPanelAction;
		_eatingPersona += EatingPersona;
		_drinkingPersona += DrinkingPersona;
		_healingPersona += HealingPersona;
	}

	public void OpenPanelAction()
	{
		_canvasManager._panelActionMain.UpdateActionPanel(this);
	}
	public void DailyNeeds()
	{
		if(_personaStats._hunger > 0)
			_personaStats._hunger--;

		if(_personaStats._thirst > 0)
			_personaStats._thirst--;

		if(_personaStats._hunger == 0 || _personaStats._thirst == 0)
			_personaStats._life--;

		if(_personaStats._life <= 0)
			KillPersona();
	}
	public void EatingPersona()
	{
		if(_gameManager._mainRessources._meats > 0)
		{
			//Debug.Log(_nameOfPersona + " à manger");
			_gameManager._mainRessources._meats--;
			_personaStats._hunger++;

			_canvasManager._panelActionMain.UpdateActionPanel(this);
			//_canvasManager.UpdateRessourcesBar(_gameManager._mainRessources);
			_depositManager.UpdateDeposit();
		}
	}
	public void DrinkingPersona()
	{
		if (_gameManager._mainRessources._waters > 0)
		{
			_gameManager._mainRessources._waters--;
			_personaStats._thirst++;

			_canvasManager._panelActionMain.UpdateActionPanel(this);
			//_canvasManager.UpdateRessourcesBar(_gameManager._mainRessources);
			_depositManager.UpdateDeposit();
		}
	}
	public void HealingPersona()
	{
		if (_gameManager._mainRessources._plants > 0)
		{
			_gameManager._mainRessources._plants--;
			_personaStats._life++;

			_canvasManager._panelActionMain.UpdateActionPanel(this);
			//_canvasManager.UpdateRessourcesBar(_gameManager._mainRessources);
			_depositManager.UpdateDeposit();
		}
	}
	public void ChooseActionPersona()
	{
		for (int i = 0; i < _listOfActions.Count; i++)
		{
			if(_canvasManager._dropdownAction.value == i)
			{
				_currentAction = _listOfActions[i];
				Debug.Log("Action de " + _nameOfPersona + ": " + _currentAction._actionName);
			}
		}
	}
	public void ActionPersona()
	{
		Debug.Log("Action de " + _nameOfPersona);
		if(_currentAction != null)
		{
			_gameManager._mainRessources._meats += _currentAction._ressourcesStr._meats;
			_gameManager._mainRessources._plants += _currentAction._ressourcesStr._plants;
			_gameManager._mainRessources._skins += _currentAction._ressourcesStr._skins;
			_gameManager._mainRessources._stones += _currentAction._ressourcesStr._stones;
			_gameManager._mainRessources._waters += _currentAction._ressourcesStr._waters;
			_gameManager._mainRessources._woods += _currentAction._ressourcesStr._woods;

			_personaStats._life += _currentAction._personaStr._life;
			_personaStats._hunger += _currentAction._personaStr._hunger;
			_personaStats._thirst += _currentAction._personaStr._thirst;
			_personaStats._bodyTemperature += _currentAction._personaStr._bodyTemperature;
		}
	}
	public void KillPersona()
	{
		_gameManager._listOfPersona.Remove(this);
		_gameManager._listOfPersonasGO.Remove(this.gameObject);
		Destroy(this.gameObject);
	}
}
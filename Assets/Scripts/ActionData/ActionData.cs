using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Action")]
public class ActionData : ScriptableObject
{
	//public enum EModificator
	//{
	//	EFood,
	//	EWood,
	//	ELife
	//}
	//[System.Serializable]
	//public class Modificator
	//{
	//	public EModificator modificator;
	//	public int value;
	//}
	//public List<Modificator> modificators;
	public string _actionName;
	public RessourcesStr _ressourcesStr;
	public PersonaStr _personaStr;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Persona", menuName = "New Persona")]
public class PersonaData : ScriptableObject
{
	public string _namePersona;
	public Sprite _spriteOfPersona;
	public PersonaStr _personaStr;
	public List<ActionData> _listOfActions;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventData : ScriptableObject
{
	public string _eventName;
	[TextArea] public string _eventDescription;
	public RessourcesStr _ressourcesStr;
	public PersonaStr _personaStr;
}

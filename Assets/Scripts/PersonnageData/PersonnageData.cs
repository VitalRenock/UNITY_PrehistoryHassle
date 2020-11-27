/*
 * Persona:
 *		Nom
 *		Histoire
 *		Vie
 *		Faim
 *		Soif
 *		
 *	Maladies:
 *		Faim++
 *		Soif++
 *		Empoisonement
 *		Grippe
 *	
 *	Talents:
 *		Chasse - Furtive
 *		Chasse - Gros Gibier
 *		Construction - économique ressources /2
 *		Construction - apprentissage recettes *2
 *		Survie - Métabolisme lent Faim et Soif ralentis
 *		Survie - Résistance aux maladies
 *	
 *	Actions Quotidienne:
 *		Manger
 *		Boire
 *		S'équiper
 *		
 *	Actions Spéciale:
 *		Se soigner
 *		Se reposer
 *		Chasser
 *		Cueillir
 *		Fabriquer
 *		
 *	Equipements:
 *		Durabilité?
 *		Torse
 *		Main
 *		Pieds
 *		
 *	Fabrications:
 *		Baton
 *		Corde
 *		Lance
 *		Hache
 *		Feu de camp
 *		Séchoir
 *		Poncho
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ressource
{
	Croquette,
	Pochette,
	Viande
}

[CreateAssetMenu(fileName = "New Personnage" , menuName = "New Personnage")]
public class PersonnageData : ScriptableObject
{
	public string _name;
	public int _life;
	public int _hunger;
	public int _thirst;

	[System.Serializable]
	public class ActionPersona
	{
		public string _actionName;
		public Ressource _actionRessource;
		public int _actionValue;
	}
	public List<ActionPersona> _actionPersonas;
}

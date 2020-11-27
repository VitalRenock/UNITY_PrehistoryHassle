using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAction : MonoBehaviour
{
	public Text _textTitle;
	public Dropdown _actionsDropdown;
	public Text _textStatsPersona;

	public Button _eatButton;
	public Button _drinkButton;
	public Button _healButton;

	private void Awake()
	{
		// PanelAction récupere ses composants
		_textTitle = transform.Find("Title").GetComponent<Text>();
		_actionsDropdown = transform.Find("Dropdown").GetComponent<Dropdown>();
		_textStatsPersona = transform.Find("Text Of Stats").GetComponent<Text>();
		_eatButton = transform.Find("Panel Buttons").Find("Eat Button").GetComponent<Button>();
		_drinkButton = transform.Find("Panel Buttons").Find("Drink Button").GetComponent<Button>();
		_healButton = transform.Find("Panel Buttons").Find("Heal Button").GetComponent<Button>();
	}

	public void UpdateActionPanel(Persona personaSend)
	{
		#region Vérification ouverture Panel
		if (gameObject.activeSelf == false)
			gameObject.SetActive(true);
		#endregion

		#region Maj Titre
		// Mise à jour du nom du Persona
		_textTitle.text = personaSend._nameOfPersona;
		#endregion

		#region Maj Dropdown
		// Nettoyage des options de la DropDown
		_actionsDropdown.ClearOptions();
		// Nouvelles options pour la DropDown en fonction des actions posssible du Persona
		List<string> dropDownOptions = new List<string>();
		foreach (ActionData item in personaSend._listOfActions)
		{
			dropDownOptions.Add(item._actionName);
		}
		// Ajout des nouvelles options dans la DropDown
		_actionsDropdown.AddOptions(dropDownOptions);
		_actionsDropdown.onValueChanged.RemoveAllListeners();
		_actionsDropdown.onValueChanged.AddListener(delegate { personaSend.ChooseActionPersona(); });
		#endregion

		#region Maj Statistiques
		int personaLife = personaSend._personaStats._life;
		int personaHunger = personaSend._personaStats._hunger;
		int personaThirst = personaSend._personaStats._thirst;

		string newText = "";

		if (personaLife <= 3)
			newText += "- Je me sens vraiment mal!";
		else if (personaLife > 3 && personaLife <= 5)
			newText += "- C'est pas la forme aujourd'hui.";
		else if (personaLife > 5 && personaLife <= 8)
			newText += "- Tout va bien.";
		else if (personaLife > 8)
			newText += "- Je suis au top là.";

		newText += "\n\n";

		if (personaHunger <= 3)
			newText += "- Je meurs de faim!";
		else if (personaHunger > 3 && personaHunger <= 5)
			newText += "- Je mangerais un buffle si je pouvais.";
		else if (personaHunger > 5 && personaHunger <= 8)
			newText += "- Hummm, j'ai un petit creux.";
		else if (personaHunger > 8)
			newText += "- Je suis repus.";

		newText += "\n\n";

		if (personaThirst <= 3)
			newText += "- Je suis complètement désydrater!";
		else if (personaThirst > 3 && personaThirst <= 5)
			newText += "- J'ai très soif.";
		else if (personaThirst > 5 && personaThirst <= 8)
			newText += "- Une petite gorgée d'eau me ferait du bien.";
		else if (personaThirst > 8)
			newText += "- Je ne veux plus rien boire.";

		_textStatsPersona.text = newText;
		#endregion

		#region Maj Buttons
		_eatButton.onClick.RemoveAllListeners();
		_eatButton.onClick.AddListener(personaSend._eatingPersona);
		_drinkButton.onClick.RemoveAllListeners();
		_drinkButton.onClick.AddListener(personaSend._drinkingPersona);
		_healButton.onClick.RemoveAllListeners();
		_healButton.onClick.AddListener(personaSend._healingPersona);
		#endregion
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deposit : MonoBehaviour
{
	public enum RessourcesType
	{
		Meats,
		Plants,
		Skins,
		Stones,
		Waters,
		Woods
	}
	public RessourcesType _ressourcesType;
	public Sprite _spriteRessource;
	public Vector2 _sizeOfSprite;
	GameManager _gameManager;

	List<GameObject> _listRessourcesGO;

	private void Awake()
	{
		_gameManager = FindObjectOfType<GameManager>();
	}

	private void Start()
	{
		InstantiateDeposit();
	}

	public void InstantiateDeposit()
	{
		_listRessourcesGO = new List<GameObject>();

		RectTransform rectTransformParent = GetComponent<RectTransform>();
		int stock = new int();

		switch (_ressourcesType)
		{
			case RessourcesType.Meats:
				stock = _gameManager._mainRessources._meats;
				break;

			case RessourcesType.Plants:
				stock = _gameManager._mainRessources._plants;
				break;

			case RessourcesType.Skins:
				stock = _gameManager._mainRessources._skins;
				break;

			case RessourcesType.Stones:
				stock = _gameManager._mainRessources._stones;
				break;

			case RessourcesType.Waters:
				stock = _gameManager._mainRessources._waters;
				break;
			case RessourcesType.Woods:
				stock = _gameManager._mainRessources._woods;
				break;

			default:
				Debug.LogError("No ressources choose in the switch!");
				break;
		}

		for (int i = 0; i < stock; i++)
		{
			GameObject spriteRessourceGO = new GameObject();
			spriteRessourceGO.transform.SetParent(transform);
			spriteRessourceGO.name = "Sprite Ressource " + i.ToString();

			float posX = Random.Range(0, rectTransformParent.rect.size.x);
			float posY = Random.Range(0, rectTransformParent.rect.size.y);

			RectTransform rectTransform = spriteRessourceGO.AddComponent<RectTransform>();
			rectTransform.localPosition = new Vector3(posX, posY, 0);
			rectTransform.sizeDelta = new Vector2(_sizeOfSprite.x, _sizeOfSprite.y);

			Image spriteImage = spriteRessourceGO.AddComponent<Image>();
			spriteImage.sprite = _spriteRessource;

			_listRessourcesGO.Add(spriteRessourceGO);
		}
	}

	public void UpdateDeposit()
	{
		foreach (GameObject item in _listRessourcesGO)
		{
			Destroy(item);
		}

		RectTransform rectTransformParent = GetComponent<RectTransform>();
		int stock = new int();

		switch (_ressourcesType)
		{
			case RessourcesType.Meats:
				stock = _gameManager._mainRessources._meats;
				break;

			case RessourcesType.Plants:
				stock = _gameManager._mainRessources._plants;
				break;

			case RessourcesType.Skins:
				stock = _gameManager._mainRessources._skins;
				break;

			case RessourcesType.Stones:
				stock = _gameManager._mainRessources._stones;
				break;

			case RessourcesType.Waters:
				stock = _gameManager._mainRessources._waters;
				break;
			case RessourcesType.Woods:
				stock = _gameManager._mainRessources._woods;
				break;

			default:
				Debug.LogError("No ressources choose in the switch!");
				break;
		}

		for (int i = 0; i < stock; i++)
		{
			GameObject spriteRessourceGO = new GameObject();
			spriteRessourceGO.transform.SetParent(transform);
			spriteRessourceGO.name = "Sprite Ressource " + i.ToString();

			float posX = Random.Range(0, rectTransformParent.rect.size.x);
			float posY = Random.Range(0, rectTransformParent.rect.size.y);

			RectTransform rectTransform = spriteRessourceGO.AddComponent<RectTransform>();
			rectTransform.localPosition = new Vector3(posX, posY, 0);
			rectTransform.sizeDelta = new Vector2(_sizeOfSprite.x, _sizeOfSprite.y);

			Image spriteImage = spriteRessourceGO.AddComponent<Image>();
			spriteImage.sprite = _spriteRessource;

			_listRessourcesGO.Add(spriteRessourceGO);
		}
	}

	public void TestAffichage()
	{
		Debug.Log(_gameManager._mainRessources._meats);
	}
}

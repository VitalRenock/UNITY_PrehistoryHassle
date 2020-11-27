using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursivity : MonoBehaviour
{
	enum Estate
	{
		EBomb,
		ECitoyen,
		ENothing
	}
	List<Estate> Pays;
	List<int> indexes;


	private void Start()
	{
		FirstRecursive(4);

		Debug.Log(Factorielle(52));
	}

	void FirstRecursive(int index)
	{
		indexes.Clear();
		FirstRecursive(index);

	}
	void Recursivities(int index)
	{
		if (index < 0 || index >= Pays.Count)
			return;
		indexes.Add(index);

		switch (Pays[index])
		{
			case Estate.EBomb:
				Pays[index] = Estate.ENothing;
				Recursivities(index - 1);
				Recursivities(index + 1);
				break;

			case Estate.ECitoyen:
				Pays[index] = Estate.ENothing;
				break;

			case Estate.ENothing:
				return;

			default:
				break;
		}
	}


	int Factorielle(int i)
	{
		if (i == 1)
			return 1;

		return Factorielle(i - 1) * i;
	}
}

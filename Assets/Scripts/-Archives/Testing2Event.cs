using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing2Event : MonoBehaviour
{
	TestingEvent testingEventScript;

	// Souscription du Listener (tjs souscrire les listeners avant tout le monde
	private void Awake()
	{
		testingEventScript = FindObjectOfType<TestingEvent>();

		testingEventScript.myFirstEvent += MonPremierListener;
		testingEventScript.mySecondEvent += MonDeuxiemeListener;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			testingEventScript.OnSecondEvent(new TestingEvent.MyFirstEventArgs() { monInt = 3});
		

		//testingEventScript.MyfirstEvent(new TestingEvent.MyFirstEventArgs() { });
	}


	// Listener
	void MonPremierListener(object sender, TestingEvent.MyFirstEventArgs e)
	{
		Debug.Log(e.monInt.ToString());
	}

	public void MonDeuxiemeListener(object sender, TestingEvent.MyFirstEventArgs e)
	{
		e.AddInt();

		Debug.Log(e.monInt.ToString() + " / 2ème");
	}
}

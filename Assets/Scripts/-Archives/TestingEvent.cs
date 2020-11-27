using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestingEvent : MonoBehaviour
{
	// Création de l'event
    public class MyFirstEventArgs : EventArgs
	{
		public int monInt;

		public void AddInt()
		{
			monInt++;
		}
	}
	public event EventHandler<MyFirstEventArgs> myFirstEvent;
	public event EventHandler<MyFirstEventArgs> mySecondEvent;


	// Moyen de communiquer avec l'event, lance l'event.
	public void OnFirstEvent(MyFirstEventArgs e)
	{
		myFirstEvent?.Invoke(this, e);
	}
	public void OnSecondEvent(MyFirstEventArgs e)
	{
		mySecondEvent?.Invoke(this, e);
	}





	// Utilistation de l'event
	private void Start()
	{
		//MyfirstEvent(new MyFirstEventArgs() { monInt = 0 });

	}

	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Space))
		//	MyfirstEvent(new MyFirstEventArgs() { });
	}
}

using UnityEngine;
using System.Collections;

public class Combo{

	public KeyCode[] keycodes;
	private int currentIndex = 0; //moves along the array as buttons are pressed
	
	public float allowedTimeBetweenButtons = 0.2f; //tweak as needed
	private float timeLastButtonPressed;
	
	public Combo(KeyCode[] b)
	{
		keycodes = b;
	}
	
	//usage: call this once a frame. when the combo has been completed, it will return true
	public bool Check()
	{
		if (Time.time > timeLastButtonPressed + allowedTimeBetweenButtons) currentIndex = 0;
		{
			if (currentIndex < keycodes.Length)
			{
				if ((keycodes[currentIndex] == KeyCode.S && Input.GetKeyDown(KeyCode.S)) ||
				    (keycodes[currentIndex] == KeyCode.W && Input.GetKeyDown(KeyCode.W)) ||
				    (keycodes[currentIndex] == KeyCode.A && Input.GetKeyDown(KeyCode.A)) ||
				    (keycodes[currentIndex] == KeyCode.D && Input.GetKeyDown(KeyCode.D)) ||
				    (keycodes[currentIndex] != KeyCode.S && keycodes[currentIndex] != KeyCode.W && keycodes[currentIndex] != KeyCode.A && keycodes[currentIndex] != KeyCode.D && Input.GetKeyDown(keycodes[currentIndex])))
				{
					timeLastButtonPressed = Time.time;
					currentIndex++;
				}
				
				if (currentIndex >= keycodes.Length)
				{
					currentIndex = 0;
					return true;
					
				}
				else return false;
			}
		}
		
		return false;
	}
}

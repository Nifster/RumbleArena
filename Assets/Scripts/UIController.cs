using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour {

	//This script is meant to change values on the UI according to other things
	[Header("UI Objects")]
	public Text nameText;
	public Text hungerText;
	public Text expText;

	[Header("Source Objects")]
	public GameObject petObject;
	List<string> uiValues;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		uiValues = petObject.GetComponent<CreatureLogic>().uiValues;
		//store all values in a list from creature logic and get them all in one go using getcomponent
		nameText.text = uiValues[0];
		hungerText.text = uiValues[1];
		expText.text = uiValues[2];
	}

}

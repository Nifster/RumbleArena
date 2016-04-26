using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	public RuntimeAnimatorController[] animatorList = new RuntimeAnimatorController[20];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SwitchAnimation(Animator currAnim, string parameterName, bool isActive){
		currAnim.SetBool(parameterName,isActive);
	}

	public void ToggleAnimation(Animator currAnim, bool state){
		currAnim.enabled = state;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

public class CreatureLogic : MonoBehaviour {
	
	[Header("Digi Attributes")]
	public string m_name;
	public float m_hunger;
	public float m_experience;
	public int m_lifeCycleCount;
	public float m_attackStat;
	public float m_defenseStat;
	public float m_healthCurrent;
	public float m_healthMax;
	public int numOfVisibleAttributes;

	bool isFacingRight = true;
	Vector2 lastPos;
	
	
	//[HideInInspector]
	public enum Attribute{
		Vaccine = 0,
		Data,
		Virus
	}	
	
	public enum Family{
		Dragon = 0,
		Holy,
		Beast,
		Insect,
		Aqua,
		Darkness
	}
	
	public enum Stage{
		Fresh = 0,
		InTraining,
		Child,
		Adult,
		Perfect,
		Ultimate
	}

	public int[] ExpRequirements;
	
	public Attribute m_digiAttribute;
	public Family m_digiFamily;
	public Stage m_digiStage;

	Timers myTimers;
	Movement myMovement;
	AnimationController myAnimController;
	Animator myAnimator;
	Image myImage;
	EvolutionController myEvolutionController;

	[HideInInspector]
	public List<string> uiValues;
	
	// Use this for initialization
	void Start(){
		myTimers = this.GetComponent<Timers>();
		myMovement = this.GetComponent<Movement>();
		myAnimator = this.GetComponent<Animator>();
		myImage = this.GetComponent<Image>();
		myAnimController = this.GetComponent<AnimationController>();
		myEvolutionController = this.GetComponent<EvolutionController>();
		ExpRequirements = new int[6]{10,20,30,40,50,60};
		uiValues = uiValues = new List<string>(){m_name,m_hunger.ToString(),m_experience.ToString()};

	}
	
	// Update is called once per frame
	void Update(){
		uiValues = uiValues = new List<string>(){m_name,m_hunger.ToString(),m_experience.ToString()};

		if(!myTimers.isHatched){
			myMovement.ResetPosition();
			myAnimController.SwitchAnimation(myAnimator,"isHatched",false);
			myTimers.HatchDigiEgg();
			if(myTimers.m_resetAttributes){
				ResetAttributes ();
				myTimers.m_resetAttributes = false;
			}

		}else{
			myTimers.MovementTimer();
			myAnimController.SwitchAnimation(myAnimator,"isHatched",myTimers.isHatched);
			myAnimController.SwitchAnimation(myAnimator,"movementCheck",myTimers.movementCheck);
			if(myTimers.movementCheck){
				myMovement.RandomMovement();
			}
			myMovement.TouchMovement();
			myTimers.LifetimeTick();
		}

		//checks which direction pet is moving and flips sprite accordingly
		if((this.transform.position.x > lastPos.x) && !isFacingRight){
			Vector3 newScale = gameObject.transform.localScale;
			newScale.x *= -1;
			gameObject.transform.localScale = newScale;
			isFacingRight = true;
			Debug.Log ("Moving Right -->");
		}else if((this.transform.position.x < lastPos.x) && isFacingRight){
			Vector3 newScale = gameObject.transform.localScale;
			newScale.x *= -1;
			gameObject.transform.localScale = newScale;
			isFacingRight = false;
			Debug.Log("<-- Moving Left");
		}

		lastPos = this.transform.position;


		
	}

	void ResetAttributes ()
	{
		m_lifeCycleCount++;
		m_digiAttribute = (Attribute)Random.Range ((int)Attribute.Vaccine, (int)Attribute.Virus + 1);
		m_digiFamily = (Family)Random.Range ((int)Family.Dragon, (int)Family.Darkness + 1);
		m_digiStage = Stage.Fresh;
		m_experience = 0;
		myAnimator.runtimeAnimatorController = myAnimController.animatorList[0];
	}


	public void AddExp(){
		int addAmount = 10;
		m_experience += addAmount;
		if(m_experience >= ExpRequirements[(int)m_digiStage]){
			JSONNode evolvedPet = myEvolutionController.CheckBranchEvolution(m_name,m_attackStat,m_defenseStat,m_lifeCycleCount);
			m_name = evolvedPet["Name"];
			m_digiAttribute = (Attribute)evolvedPet["Attribute"].AsInt;
			m_digiFamily = (Family)evolvedPet["Family"].AsInt;
			m_digiStage++;
			//TODO: Replace testAnimator with index based array for animatorcontrollers
			//to be replaced by ID of evolution
			myAnimator.runtimeAnimatorController = myAnimController.animatorList[evolvedPet["ID"].AsInt];
		}
	}

}

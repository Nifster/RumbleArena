using UnityEngine;
using System.Collections;

public class Timers : MonoBehaviour {


	[Header("DigiEgg Timer Settings")]
	public bool isHatched;
	public float m_hatchTimerMax;
	public float m_hatchTimer;
	public bool m_resetAttributes = false;

	[Header("LifeExpectancy Timer Settings")]
	public float m_lifeTick; //how long does it take to reduce one unit of lifetime
	public float m_lifeTickMax;
	public float m_lifeExpectancy;
	public float m_lifeExpectancyMax;

	[Header("Movement Timer Settings")]
	public float movementTimer = 0;
	public float lowerPauseDurationLimit;
	public float higherPauseDurationLimit;
	public bool movementCheck;

	// Use this for initialization
	void Start(){

	}
	
	// Update is called once per frame
	void Update(){
		


	}

	public void HatchDigiEgg ()
	{
		//only applies to creaturelogic
		m_hatchTimer -= Time.deltaTime;
		if (m_hatchTimer <= 0) {
			isHatched = true;
			m_resetAttributes = true;
			print ("Hatched!");
			m_lifeExpectancy = m_lifeExpectancyMax;

			//reset attributes
//			CreatureLogic myCreature = this.GetComponent<CreatureLogic>();
//			myCreature.m_lifeCycleCount++;
//			myCreature.m_digiAttribute = 
//				(CreatureLogic.Attribute)Random.Range((int)CreatureLogic.Attribute.Vaccine,(int)CreatureLogic.Attribute.Virus+1);
//			myCreature.m_digiFamily = 
//				(CreatureLogic.Family)Random.Range((int)CreatureLogic.Family.Dragon, (int)CreatureLogic.Family.Darkness+1);
//			myCreature.m_digiStage = CreatureLogic.Stage.Fresh;
		}
	}

	public void LifetimeTick(){

		m_lifeTick -= Time.deltaTime;
		if(m_lifeTick <= 0){
			m_lifeExpectancy--;
			m_lifeTick = m_lifeTickMax;
		}
		
		if(m_lifeExpectancy <= 0){
			isHatched = false;
			m_hatchTimer = m_hatchTimerMax;
		}
	}

	public void MovementTimer(){
		if(Time.time >= movementTimer){
			movementCheck= !movementCheck;
			movementTimer = Time.time + Random.Range(lowerPauseDurationLimit,higherPauseDurationLimit);
		}
	}
	
}

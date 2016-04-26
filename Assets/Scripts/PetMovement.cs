using UnityEngine;
using System.Collections;

public class Pet{
	public float hunger;
	public float bowels;
	public float fatigue;

	public Pet(){
		hunger = 0;
		bowels = 0;
		fatigue = 0;
	}

	public Pet(float hungerValue, float bowelsValue, float fatigueValue){
		hunger = hungerValue;
		bowels = bowelsValue;
		fatigue = fatigueValue;
	}

}

public class PetMovement : MonoBehaviour {

	Pet myPet;
	float tChange = 0;
	public float randomMoveThreshold = 0;
	private float randomX;
	private float randomY;
	public float moveSpeed;
	public float horizontalLimitOffset;
	public float verticalLimitOffset;
	float leftCameraLimit;
	float rightCameraLimit;
	float topCameraLimit;
	float botCameraLimit;
	float timer = 0;
	public float lowerPauseThreshold;
	public float higherPauseThreshold;
	public float lowerMoveThreshold;
	public float higherMoveThreshold;
	bool movementCheck;
	bool isFacingRight = true;
	Vector2 lastPos;

	[Header ("Hunger Settings")]
	public float hungerInterval; //interval between hunger value reduction
	public float hungerReductionRate; //how much hunger value reduces per tick
	float hungerTimer; //just a timer, temp
	public float hungerValue;
	public float foodValue;

	[Header("Bowels Settings")]
	public float bowelsValue;

	[Header("Fatigue Settings")]
	public float fatigueValue;

	// Use this for initialization
	void Start(){

		leftCameraLimit = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
		rightCameraLimit = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;
		topCameraLimit = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).y;
		botCameraLimit = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y;
		movementCheck = true;
		lastPos = this.transform.position;

		hungerValue = PlayerPrefs.GetFloat("Hunger",10f);
		bowelsValue = PlayerPrefs.GetFloat("Bowels", 10f);
		fatigueValue = PlayerPrefs.GetFloat("Fatigue",10f);
		myPet = new Pet(hungerValue,bowelsValue,fatigueValue);
	
	}
	
	// Update is called once per frame
	void Update(){

		//random movement around the room
		if(Time.time >= timer){
			movementCheck= !movementCheck;
			timer = Time.time + Random.Range(lowerPauseThreshold,higherPauseThreshold);
		}
		if(movementCheck){
			gameObject.GetComponent<Animator>().SetBool("movementCheck",true);
			RandomMovement();
		}else{
			gameObject.GetComponent<Animator>().SetBool("movementCheck",false);
		}

		//reduce hunger value over time
		hungerTimer -= Time.deltaTime;
		if(hungerTimer <= 0){
			myPet.hunger -= hungerReductionRate;
			hungerTimer = hungerInterval;
		}
		Debug.Log("Hunger " + myPet.hunger);

		if(myPet.hunger <=5){
			Debug.Log("I'm hungry!");
		}
		PlayerPrefs.SetFloat("Hunger",myPet.hunger);

		//Flip sprite depending on which way pet is moving
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

	void RandomMovement(){


		// change to random direction at random intervals
		if (Time.time >= tChange){
			randomX = Random.Range(-randomMoveThreshold,randomMoveThreshold); // with float parameters, a random float
			randomY = Random.Range(-randomMoveThreshold,randomMoveThreshold); //  between -2.0 and 2.0 is returned
			// set a random interval between 0.5 and 1.5
			tChange = Time.time + Random.Range(lowerMoveThreshold,higherMoveThreshold);
		}

		transform.Translate(new Vector3(randomX,randomY,0) * moveSpeed * Time.deltaTime);
		// if object reached any border, revert the appropriate direction
		if (transform.position.x >= rightCameraLimit - horizontalLimitOffset 
		    || transform.position.x <= leftCameraLimit + horizontalLimitOffset) {
			randomX = -randomX;
		}
		if (transform.position.y >= topCameraLimit - verticalLimitOffset 
		    || transform.position.y <= botCameraLimit + verticalLimitOffset) {
			randomY = -randomY;
		}
		Mathf.Clamp(transform.position.x, leftCameraLimit, rightCameraLimit);
		Mathf.Clamp(transform.position.y, botCameraLimit, topCameraLimit);
	}

	public void ResetStats(){
		PlayerPrefs.SetFloat("Hunger",10f);
		PlayerPrefs.SetFloat("Bowels", 10f);
		PlayerPrefs.SetFloat("Fatigue",10f);
		myPet.hunger = PlayerPrefs.GetFloat("Hunger",10f);
		myPet.bowels = PlayerPrefs.GetFloat("Bowels", 10f);
		myPet.fatigue = PlayerPrefs.GetFloat("Fatigue",10f);
	}

	public void Feed(){
		myPet.hunger += foodValue;
	}
}

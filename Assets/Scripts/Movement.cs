using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


	[Header("Movement Settings")]
	public float xMinLimit;
	public float xMaxLimit;
	public float yMinLimit;
	public float yMaxLimit;
	private float randomX;
	private float randomY;
	public float moveSpeed;
	public float lowerMoveThreshold;
	public float higherMoveThreshold;
	float tChange = 0;
	public float originX = 0;
	public float originY = 0;

	bool randomMoveCheck = true;

	//public bool isFacingLeft = false;
	//float resetPositionSpeed = 3f;

	// Use this for initialization
	void Start(){
	
	}
	
	// Update is called once per frame
	void Update(){
	
	}

	public void RandomMovement ()
	{
		if(randomMoveCheck){
			if (Time.time >= tChange) {
				
				randomX = Random.Range (xMinLimit, xMaxLimit);
				// with float parameters, a random float
				randomY = Random.Range (yMinLimit,yMaxLimit);
				//  between -2.0 and 2.0 is returned
				// set a random interval between 0.5 and 1.5
				tChange = Time.time + Random.Range (lowerMoveThreshold, higherMoveThreshold);
				/*if(randomX < transform.position.x){
					isFacingLeft = true;
				}else{
					isFacingLeft = false;
				}*/ //attempted to do sprite facing logic in this script, end up doing on main logic script
			}
			float step = moveSpeed*Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position,new Vector3(randomX,randomY,0),step);
			//transform.Translate (new Vector3 (randomX, randomY, 0) * moveSpeed * Time.deltaTime);
		}
	}

	public void ResetPosition(){
		//resets position to center
		//float step = resetPositionSpeed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position,new Vector3(originX,originY,0),step);
		//above is alternative code, to translate object to center
		transform.position = new Vector3(originX,originY,0);
	}

	public void TouchMovement(){
		//touch input logic, turns off random movement until after pet moves to position specified by touch
		/*if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began){
			randomMoveCheck = false;
			float step = moveSpeed*Time.deltaTime;
			Debug.Log(Input.GetTouch(0).position);
			transform.position = Vector3.MoveTowards(transform.position,Input.GetTouch(0).position,step);
			randomMoveCheck=true;
		}else{
			randomMoveCheck=true;
		}*/
		if(Input.GetMouseButtonDown(0)){
			randomMoveCheck = false;
			float step = moveSpeed*Time.deltaTime;
			Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			position.z =0;
			Debug.Log(position);
			transform.position = Vector3.MoveTowards(transform.position,position,step);
			//randomMoveCheck=true;
		}else{
			randomMoveCheck=true;
		}
	}
}

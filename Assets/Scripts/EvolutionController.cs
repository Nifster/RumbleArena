using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class EvolutionController : MonoBehaviour {


	public TextAsset evolutionTreeText;
	string evoTreeString;
	JSONNode evoTreeJSON;
	JSONNode petListJSON;
	List<JSONNode> evolutionList;

	// Use this for initialization
	void Start () {
		evoTreeString = evolutionTreeText.ToString();
		evoTreeJSON = JSON.Parse(evoTreeString);

		petListJSON = evoTreeJSON["EvolutionTree"];
		//Debug.Log(evolutionsJSON.Count);
		List<JSONNode> evolutionList = new List<JSONNode>();

//		for(int i=0; i<petListJSON.Count;i++){
//			Debug.Log(petListJSON[i]["Name"]);
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		

	public JSONNode CheckBranchEvolution(string name, float attackStat, float defStat, int lifeCycleCount){
		//look into text file of evolutions (or server, if later on i choose to store the branch evolution info somewhere more secure)
		//Based on the stats above, find which digimon it will evolve into
		//look through array of names, find name of current digimon
		//check if minimum stat requirements are met for each digimon in the sub-array
		//if yes, and only one met, return name of that digimon
		//if yes and more than one met, choose the one closest to actual values
		//if no, (only applies for ultimate and above), return null, i.e digimon dies
		//Debug.Log(name.Equals(petListJSON[0]["Name"]));
		for(int i=0; i<petListJSON.Count;i++){
			Debug.Log("Check");
			if(name.Equals(petListJSON[i]["Name"])){
				Debug.Log(petListJSON[i]["Name"]);
				for(int j=0; j<petListJSON[i]["Evolutions"].Count;j++){
					if(attackStat >= petListJSON[i]["Evolutions"][j]["Attack"].AsFloat){
						if(defStat >= petListJSON[i]["Evolutions"][j]["Defense"].AsFloat){
							return petListJSON[i]["Evolutions"][j];
						}
					}
				}

			}
		}
		return null;
	}


}

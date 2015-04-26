
using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {
	
	
	public GameObject targetLight;
	private bool  playerInReach;
	
	void  Update (){
		if(playerInReach && Input.GetKeyDown("e")) {
			targetLight.SendMessage("ToggleLight");
		}
	}
	
	void  OnTriggerEnter ( Collider collider  ){
		if(collider.gameObject.tag == "Player")
			playerInReach = true;
		
	}
	
	void  OnTriggerExit ( Collider collider  ){
		if(collider.gameObject.tag == "Player")
			playerInReach = false;
	}
}
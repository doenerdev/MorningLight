
using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {
	
	
	private Collider colliderBox;
	private Light lightComponent;
	public float regenerationSpeed = 0.1f;
	public float regenerationAmountMax = 30.0f;
	public GameObject player;
	
	void  Start (){
		colliderBox = GetComponent<Collider>();
		lightComponent = GetComponent<Light>();
	}
	
	void  ToggleLight (){
		colliderBox.enabled = !colliderBox.enabled;
		lightComponent.enabled = !lightComponent.enabled;
	}
	
	void  OnTriggerEnter ( Collider collider  ){
		if(collider.gameObject.tag == "Enemy") {
			Destroy(collider.gameObject);
		}
		else if(collider.gameObject.tag == "Player") {
			Vector2 regenerationData = new Vector2(regenerationSpeed, regenerationAmountMax); 
			player.SendMessage("EnteringLight", regenerationData);
		}
	}
	
	void OnTriggerExit(Collider collider) {
		if(collider.gameObject.tag == "Player") {
			player.SendMessage("EnteringDarkness");
		}
	}
}

using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {
	
	
	private Collider colliderBox;
	private Light lightComponent;
	public float regenerationAmount = 0.1f;
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
			player.SendMessage("EnteringLight", regenerationAmount);
		}
	}
	
	void OnTriggerExit(Collider collider) {
		if(collider.gameObject.tag == "Player") {
			player.SendMessage("EnteringDarkness");
		}
	}
}
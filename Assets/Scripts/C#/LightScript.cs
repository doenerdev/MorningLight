
using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {
	
	
	private Collider colliderBox;
	private Light lightComponent;
	
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
	}
}
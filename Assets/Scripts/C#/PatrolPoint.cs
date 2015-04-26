
using UnityEngine;
using System.Collections;

public class PatrolPoint : MonoBehaviour {
	
	
	public GameObject patroller;
	private bool  activable = true;
	
	void  OnTriggerEnter ( Collider collider  ){
		if(collider.gameObject.tag == "Enemy" && activable) {
			patroller.SendMessage("SetNextPatrolPoint");
			activable = false;
		}
	}
	
	void  OnTriggerExit ( Collider collider  ){
		if(collider.gameObject.tag == "Enemy") {
			activable = true;
		}
	}
}
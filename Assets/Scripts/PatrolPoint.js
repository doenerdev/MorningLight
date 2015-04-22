#pragma strict

public var patroller:GameObject;
private var activable:boolean = true;

function OnTriggerEnter(collider:Collider) {
	if(collider.gameObject.tag == "Enemy" && activable) {
		Debug.Log("Enemy Entered");
		patroller.SendMessage("SetNextPatrolPoint");
		activable = false;
	}
}

function OnTriggerExit(collider:Collider) {
	if(collider.gameObject.tag == "Enemy") {
		activable = true;
	}
}
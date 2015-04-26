#pragma strict

public var targetLight:GameObject;
private var playerInReach:boolean;

function Update() {
	if(playerInReach && Input.GetKeyDown("e")) {
		targetLight.SendMessage("ToggleLight");
	}
}

function OnTriggerEnter(collider:Collider) {
	if(collider.gameObject.tag == "Player")
		playerInReach = true;
	
}

function OnTriggerExit(collider:Collider) {
	if(collider.gameObject.tag == "Player")
		playerInReach = false;
}
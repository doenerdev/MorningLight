#pragma strict

private var colliderBox:Collider;
private var lightComponent:Light;

function Start () {
	colliderBox = GetComponent.<Collider>();
	lightComponent = GetComponent.<Light>();
}

function ToggleLight() {
	colliderBox.enabled = !colliderBox.enabled;
	lightComponent.enabled = !lightComponent.enabled;
}

function OnTriggerEnter(collider:Collider) {
	if(collider.gameObject.tag == "Enemy") {
		Destroy(collider.gameObject);
	}
}
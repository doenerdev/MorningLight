#pragma strict

public var target:Transform;
public var player:Transform;
public var fieldOfView:int;
public var viewingDistance:float;
public var patrolPoints:GameObject[]; //an array holding to 3D Points for the patrol route
public var patrolPointDetectionCooldownMax:int = 200;
public var chasePlayerCooldownMax:int = 400;

private var navAgent:NavMeshAgent;
private var destination:Vector3; //the destination the enemy is heading to 
private var chasePlayer:boolean = false;
private var patrolPointCounter:int = 0;
private var patrolPointsLayerMask:LayerMask = 1 << 8;
private var lightsLayerMask:LayerMask = 1 << 9;
private var patrolPointDetectionCooldown:int = 0;
private var chasePlayerCooldown:int = 0;

function Start () {
	navAgent = GetComponent.<NavMeshAgent>();
	Patrol(); //start patrolling
}

function Update () {

	if(patrolPointDetectionCooldown > 0) {
		patrolPointDetectionCooldown--;
	}
	
	if(chasePlayerCooldownMax > 0) {
		chasePlayerCooldown--;
	}
	
	if(CanSeePlayer()) { //check whether to chase the player (if he's in line of sight and reachable) or to patrol
		chasePlayer = true;
		ChasePlayer();
	}
	else {
		if(chasePlayer && chasePlayerCooldown == 0 ) {
			Patrol(); //go back to patrolling
		}
		else if(!chasePlayer) {
			/*if(patrolPointDetectionCooldown == 0)
				CheckControlPoints();*/
			chasePlayer = false;
		}
		else {
			ChasePlayer();
			CheckForLights();
		}
	}
}

function CanSeePlayer() {	
	if(PlayerInViewingRange() && PlayerInFieldOfView() && !IsPlayerConcealed()) {
		chasePlayerCooldown = chasePlayerCooldownMax;
		return true;
	}
	return false;
}


function PlayerInViewingRange() { //check if the player is in the viewing range
	var distanceToPLayer = Mathf.Abs(Vector3.Distance(player.position, transform.position));
	return distanceToPLayer <= viewingDistance;
}

function PlayerInFieldOfView() { //check if the player is in the field of view (angle-wise)
	var heading:Vector3 = (player.position - transform.position).normalized; //direction between player and enemy
	var enemyLookingDirection:Vector3 = transform.forward; //the direction the enemy is looking in		
	var angle = Mathf.Acos(Vector3.Dot(enemyLookingDirection, heading)/(enemyLookingDirection.magnitude * heading.magnitude)) * Mathf.Rad2Deg;
	
	return angle < fieldOfView;
}

function IsPlayerConcealed() { //check if player is hidden by objects
	var hit : RaycastHit;
	if (Physics.Raycast (transform.position, (player.position - transform.position), hit)) { //check if the player isn't hidden by any objects
		if(hit.transform.gameObject.tag == "Player") {
			return false;
		}
	}
	return true;
}

function ChasePlayer() {
	destination = player.position;
	navAgent.SetDestination(destination);
}

function Patrol() {
	destination = patrolPoints[patrolPointCounter].transform.position;
	navAgent.SetDestination(destination);
}

function setPatrolDestionation(patrolPoint:int) {
	destination = patrolPoints[patrolPoint].transform.position;
	navAgent.SetDestination(destination);
}

function CheckControlPoints() {
	var hit : RaycastHit;
	if (Physics.Raycast (transform.position, transform.forward, hit, Mathf.Infinity, patrolPointsLayerMask)) { //check if the player isn't hidden by any objects
		if(Vector3.Distance(hit.point, transform.position) < 0.5) {
			Debug.Log("NewPoint");
			patrolPointDetectionCooldown = patrolPointDetectionCooldownMax;
			SetNextPatrolPoint();
		}
	}
}

function SetNextPatrolPoint() {
	if(patrolPointCounter < patrolPoints.Length-1) {
			patrolPointCounter++; //set next patrol point
	}
	else {
		patrolPointCounter = 0; //reset patrol point
	}
	Debug.Log(patrolPointCounter);
	destination = patrolPoints[patrolPointCounter].transform.position;
	navAgent.SetDestination(destination);
}

function CheckForLights() {
	var hit : RaycastHit;
	if (Physics.Raycast (transform.position, transform.forward, hit, Mathf.Infinity, lightsLayerMask)) { //check if the player isn't hidden by any objects
		if(Vector3.Distance(hit.point, transform.position) < 2) {
			chasePlayer = false;
			Patrol();
		}
	}
}

function BLA() {
	Debug.Log("hihi");
}



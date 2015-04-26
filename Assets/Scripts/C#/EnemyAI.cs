
using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	
	public Transform target;
	public Transform player;
	public int fieldOfView;
	public float viewingDistance;
	public GameObject[] patrolPoints; //an array holding to 3D Points for the patrol route
	public int patrolPointDetectionCooldownMax = 200;
	public int chasePlayerCooldownMax = 400;
	
	private NavMeshAgent navAgent;
	private Vector3 destination; //the destination the enemy is heading to 
	private bool  chasePlayer = false;
	private int patrolPointCounter = 0;
	private LayerMask patrolPointsLayerMask = 1 << 8;
	private LayerMask lightsLayerMask = 1 << 9;
	private int patrolPointDetectionCooldown = 0;
	private int chasePlayerCooldown = 0;
	
	void Start (){
		navAgent = GetComponent<NavMeshAgent>();
		Patrol(); //start patrolling
	}
	
	void Update (){
		
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
	
	bool CanSeePlayer (){	
		if(PlayerInViewingRange() && PlayerInFieldOfView() && !IsPlayerConcealed()) {
			chasePlayerCooldown = chasePlayerCooldownMax;
			return true;
		}
		return false;
	}
	
	
	bool PlayerInViewingRange (){ //check if the player is in the viewing range
		int distanceToPLayer = (int) Mathf.Abs(Vector3.Distance(player.position, transform.position));
		return distanceToPLayer <= viewingDistance;
	}
	
	bool PlayerInFieldOfView (){ //check if the player is in the field of view (angle-wise)
		Vector3 heading = (player.position - transform.position).normalized; //direction between player and enemy
		Vector3 enemyLookingDirection = transform.forward; //the direction the enemy is looking in		
		float angle= Mathf.Acos(Vector3.Dot(enemyLookingDirection, heading)/(enemyLookingDirection.magnitude * heading.magnitude)) * Mathf.Rad2Deg;
		
		return angle < fieldOfView;
	}
	
	bool IsPlayerConcealed (){ //check if player is hidden by objects
		RaycastHit hit;
		if (Physics.Raycast (transform.position, (player.position - transform.position), out hit)) { //check if the player isn't hidden by any objects
			if(hit.transform.gameObject.tag == "Player") {
				return false;
			}
		}
		return true;
	}
	
	void  ChasePlayer (){
		destination = player.position;
		navAgent.SetDestination(destination);
	}
	
	void Patrol (){
		destination = patrolPoints[patrolPointCounter].transform.position;
		navAgent.SetDestination(destination);
	}
	
	void setPatrolDestionation ( int patrolPoint  ){
		destination = patrolPoints[patrolPoint].transform.position;
		navAgent.SetDestination(destination);
	}
	
	void CheckControlPoints (){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, Mathf.Infinity, patrolPointsLayerMask)) { //check if the player isn't hidden by any objects
			if(Vector3.Distance(hit.point, transform.position) < 0.5f) {
				Debug.Log("NewPoint");
				patrolPointDetectionCooldown = patrolPointDetectionCooldownMax;
				SetNextPatrolPoint();
			}
		}
	}
	
	void SetNextPatrolPoint (){
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
	
	void CheckForLights (){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, Mathf.Infinity, lightsLayerMask)) { //check if the player isn't hidden by any objects
			if(Vector3.Distance(hit.point, transform.position) < 2) {
				chasePlayer = false;
				Patrol();
			}
		}
	}


}
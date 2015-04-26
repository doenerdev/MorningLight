using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {


	public float courageLoss = 0.3f;
	public GameObject courageUI;
	public GameObject inventorySlot;
	private float courage = 100;
	private bool isLighten = false;
	private float regenerationAmountMax = 0.0f;
	private float regenerationAmount = 0.0f;
	private float regenerationSpeed = 0.0f;

	void Update () {
		if(courage <= 0) {
			Die();
		}

		if(!isLighten) {
			decreaseCourage();
		}
		else {
			increaseCourage();
		}
	}

	void Die() {
		Application.LoadLevel(Application.loadedLevel);
	}

	void decreaseCourage() {
		courage = 0 < courage - courageLoss ? courage - courageLoss : 0;
		courageUI.SendMessage("UpdateCourage", Mathf.Floor(courage));
	}
	
	void increaseCourage() {
		courage = regenerationAmountMax > regenerationAmount ? (100 < courage + regenerationAmount ? 100 : courage + regenerationSpeed) : courage += 0;
		regenerationAmount += regenerationSpeed;
		courageUI.SendMessage("UpdateCourage", Mathf.Floor(courage));
	}
	
	void EnteringLight(Vector2 regenerationData) {
		this.regenerationSpeed += regenerationData[0];
		this.regenerationAmountMax = regenerationData[1];
		isLighten = true;
	}
	
	void EnteringDarkness() {
		regenerationAmount = 0.0f;
		regenerationSpeed = 0.0f;
		isLighten = false;
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Enemy") {
			courage = 0;
		}
	}
}

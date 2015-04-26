using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {


	public float courageLoss = 0.3f;
	public GameObject courageUI;
	private float courage = 100;
	private bool isLighten = false;
	private float regenerationAmount = 0.0f;

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
		courage = 100 < courage + regenerationAmount ? 100 : courage + regenerationAmount;
		courageUI.SendMessage("UpdateCourage", Mathf.Floor(courage));
	}
	
	void EnteringLight(float regenerationAmount) {
		this.regenerationAmount += regenerationAmount;
		isLighten = true;
	}
	
	void EnteringDarkness() {
		regenerationAmount = 0;
		isLighten = false;
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Enemy") {
			courage = 0;
		}
	}
}

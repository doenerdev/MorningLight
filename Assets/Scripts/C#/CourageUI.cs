using UnityEngine;
using System.Collections;

public class CourageUI : MonoBehaviour {

	public UnityEngine.UI.Text text;

	void UpdateCourage(int courage) {
		text.text = "Mut: " + courage;
	}
}

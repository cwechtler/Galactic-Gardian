using UnityEngine;
using System.Collections;

public class SupplyTorpedo : MonoBehaviour {

	[SerializeField] private float maxTorpedo = 1f;
	
	public float GetTorpedo(){
		return maxTorpedo;
	}
	void OnTriggerEnter2D(Collider2D collider){
		Destroy(gameObject);
	}
}

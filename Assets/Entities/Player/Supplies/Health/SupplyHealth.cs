using UnityEngine;
using System.Collections;

public class SupplyHealth : MonoBehaviour {

	[SerializeField] private float healthIncrease = 100f;
	
	public float GetHealth(){
		return healthIncrease;
	}
	void OnTriggerEnter2D(Collider2D collider){
		Destroy(gameObject);
		
	}
}

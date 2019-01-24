using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

private float delay = 40f;

	void Update () {
	delay--;
		if(delay <= 0){
			Die();
		}
	}
	
	void Die(){
		Destroy(gameObject);
	}
	
}

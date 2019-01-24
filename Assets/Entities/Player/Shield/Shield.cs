using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	
	static Shield instance = null;
	
	void Awake (){
		if (instance !=null && instance !=this) {
			Destroy (gameObject);
		}else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}		
	}
	
	void OnTriggerEnter2D(Collider2D col){
		Destroy(col.gameObject);
		
	}
}

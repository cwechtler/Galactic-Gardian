using UnityEngine;

public class TorpedoSupplyShip : MonoBehaviour {

	[SerializeField] private GameObject torpedo;
	[SerializeField] private float DropSpeed = 3;
	[SerializeField] private AudioClip shipPass;
	[SerializeField] private float volume = 0.10f;

	[SerializeField] private bool torpedoCreated = false;

	[SerializeField] private float minTime = 1.7f;
	[SerializeField] private float maxTime = 3.5f;

	//current time
	private float time;
	//The time to spawn the object
	private float spawnTime;
	
	void Start (){
		AudioSource.PlayClipAtPoint(shipPass, transform.position, volume);
		SetRandomTime();
		time = 1;
	}
	
	void FixedUpdate(){	
		//Counts up
		time += Time.deltaTime;
		if(time >= spawnTime){
			SetRandomTime();
			TorpedoDrop();
		}	
	}
	
	private void TorpedoDrop(){
		time = 0;
		if(torpedoCreated == false){
			GameObject torpedoDrop = Instantiate(torpedo, transform.position, Quaternion.identity)as GameObject;
			torpedoDrop.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -DropSpeed);
			torpedoCreated = true;
		}
	}
	//Sets the random time between minTime and maxTime
	private void SetRandomTime(){
		spawnTime = Random.Range(minTime, maxTime);
	}
}

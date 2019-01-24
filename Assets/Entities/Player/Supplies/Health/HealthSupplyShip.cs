using UnityEngine;

public class HealthSupplyShip : MonoBehaviour {

	[SerializeField] private GameObject health;
	[SerializeField] private float DropSpeed = 3;
	[SerializeField] private AudioClip shipPass;
	[SerializeField] private float volume = 0.10f;

	[SerializeField] private bool healthCreated = false;

	[SerializeField] private float minTime = 1.7f;
	[SerializeField] private float maxTime = 3.5f;

	//current time
	private float time;
	//The time to spawn the object
	private float spawnTime;
	Animator animator;

	void Start(){
		animator = GetComponent<Animator>();
		animator.Play("Supplyshipmovement");
		AudioSource.PlayClipAtPoint(shipPass, transform.position, volume);
		SetRandomTime();
		time = 1;
	}
	
	void FixedUpdate(){	
		//Counts up
		time += Time.deltaTime;
		if(time >= spawnTime){
			SetRandomTime();
			HealthShip();	
		}	
	}
		
	private void HealthShip(){
		time = 0;
		if(healthCreated == false){
			GameObject healthDrop = Instantiate(health, transform.position, Quaternion.identity)as GameObject;
			healthDrop.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -DropSpeed);
			healthCreated = true;
		}
	}
	//Sets the random time between minTime and maxTime
	private void SetRandomTime(){
		spawnTime = Random.Range(minTime, maxTime);
	}
}
	

	

	
	
	
	

	





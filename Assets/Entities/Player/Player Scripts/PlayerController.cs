using UnityEngine;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour {
	
	public static int maxTorpedos = 3;
	[SerializeField] private Text torpedoCount;

	[SerializeField] private float speed = 15.0f;
	[SerializeField] private float padding = 0.5f;

	[SerializeField] private GameObject projectile;
	[SerializeField] private GameObject torpedo;
	[SerializeField] private GameObject shield;

	[SerializeField] private float projectileSpeed;
	[SerializeField] private float firingRate = 0.2f;

	[SerializeField] private AudioClip fireSound;
	[SerializeField] private AudioClip torpedoSound;
	[SerializeField] private AudioClip torpedoUp;
	[SerializeField] private float volume = 0.4f;
	[SerializeField] private float torpedoUpVolume = 0.8f;
	
	private float minX;
	private float maxX;
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		minX = leftmost.x + padding;
		maxX = rightmost.x - padding;	
		Text torpedoCount = GetComponent<Text>();	
	}
		
	void Update () {
		MoveWithKey();
	}

	void OnTriggerEnter2D(Collider2D collider){
		SupplyTorpedo torpedo = collider.gameObject.GetComponent<SupplyTorpedo>();
		if (torpedo){
			maxTorpedos++;
			AudioSource.PlayClipAtPoint(torpedoUp, transform.position, torpedoUpVolume); 
			if(maxTorpedos >= 3){
				maxTorpedos = 3;
			}
			torpedoCount.text = maxTorpedos.ToString();
		}	
	}
					
	private void Fire(){
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity)as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position, volume);
	}
	
	private void FireTorpedo(){
		GameObject powerbeam = Instantiate(torpedo, transform.position, Quaternion.identity)as GameObject;
		powerbeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed,0);
		AudioSource.PlayClipAtPoint(torpedoSound, transform.position, volume);
		maxTorpedos--;
		if (maxTorpedos <= 0){
			maxTorpedos = 0;
		}
		torpedoCount.text = maxTorpedos.ToString();	
	}
	
	private void MoveWithKey(){

		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}

		if (Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)){
			if(maxTorpedos > 0){
				FireTorpedo();
			}			
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			LevelManager man = GameObject.Find("Level Manager").GetComponent<LevelManager>();
			man.LoadLevel("Start Menu");	
		}

		if (Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//Restrict player to gamespace
		float newX = Mathf.Clamp(transform.position.x, minX, maxX);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}

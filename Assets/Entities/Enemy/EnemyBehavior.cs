using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {

	[SerializeField] private Image currentEnemyHealthbar;
	[SerializeField] private GameObject projectile;

	[SerializeField] private float health = 150f;
	[SerializeField] private float maxHealth = 150f;
	[SerializeField] private float projectileSpeed = 10;
	[SerializeField] private float shotsPerSecond = 0.5f;
	[SerializeField] private int scoreValue = 150;
	[SerializeField] private GameObject explosion;
	[SerializeField] private AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}

	void Update(){
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Fire ();
		}
	}
	
	void UpdateHealthbar() {
		float ratio = health / maxHealth;
		currentEnemyHealthbar.rectTransform.localScale = new Vector3(ratio,1,1);	
	}
	
	void Fire(){
		GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity)as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				Die();
			}
			UpdateHealthbar();
		}
	}
	
	void Die(){
		AudioSource.PlayClipAtPoint(deathSound,transform.position);
		GameObject Explosion = Instantiate (explosion, transform.position, Quaternion.identity)as GameObject;
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
		if(EnemySpawner.enemy1Count > 0){
			EnemySpawner.enemy1Count--;
			//Debug.Log (EnemySpawner.enemy1Count);
		}
		else if((EnemySpawner.enemy1Count <= 0)&& (EnemySpawner.enemy2Count > 0)){
			EnemySpawner.enemy2Count--;
			//Debug.Log (EnemySpawner.enemy2Count);
		}
		else if((EnemySpawner.enemy1Count <= 0)&&(EnemySpawner.enemy2Count <= 0)&&(EnemySpawner.enemy3Count > 0)){
			EnemySpawner.enemy3Count--;
			//Debug.Log (EnemySpawner.enemy3Count);
		}	
	}
}

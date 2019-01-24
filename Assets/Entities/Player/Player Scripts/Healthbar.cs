using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

	[SerializeField] private Image currentHealthbar;
	[SerializeField] private Text ratioText;

	[SerializeField] private GameObject projectile;
	[SerializeField] private float hitpoint = 500f;
	[SerializeField] private float maxHitpoint = 500f;

	[SerializeField] private AudioClip deathSound;
	[SerializeField] private AudioClip healthUp;
	[SerializeField] private AudioClip takeDamage;
	[SerializeField] private float volume = 0.1f;
	[SerializeField] private float takeDamageVolume = 1f;
	
	
	void Start () {
		UpdateHealthbar();
	}

	private void UpdateHealthbar() {
		float ratio = hitpoint / maxHitpoint;
		currentHealthbar.rectTransform.localScale = new Vector3(1,ratio,1);
		ratioText.text = (ratio*100).ToString() + '%';
	}
	
	//TakeDamage & GiveHealth
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile){
			hitpoint -= missile.GetDamage();
			AudioSource.PlayClipAtPoint(takeDamage, transform.position, takeDamageVolume); 
			missile.Hit();
			if(hitpoint <= 0){	
				Die();	
			}
		}
		SupplyHealth heath = collider.gameObject.GetComponent<SupplyHealth>();
		if (heath){
			hitpoint += heath.GetHealth();
			AudioSource.PlayClipAtPoint(healthUp, transform.position,volume); 
			if(hitpoint >= 500){
				hitpoint = 500;
			}
		}
		UpdateHealthbar();	
	}
	
	private void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position); 
		Destroy (gameObject);	
		LevelManager man = GameObject.Find("Level Manager").GetComponent<LevelManager>();
		man.LoadLevel("Lose Screen");		
	}
}

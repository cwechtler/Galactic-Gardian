using UnityEngine;

public class Projectile : MonoBehaviour {
	
	[SerializeField] private float damage = 100f;
	
	public float GetDamage(){
		return damage;
	}
	public void Hit(){
		Destroy(gameObject);
	}
	void OnCollisionEnter2D(Collision2D collision){
		Destroy(gameObject);
	}
}

using UnityEngine;

public class SupplyDrop : MonoBehaviour {

	public static int supplyHealthDrop = 1725;
	public static int supplyTorpedoDrop = 3000;

	[SerializeField] private float width = 5f;
	[SerializeField] private float height = 5f;

	[SerializeField] private GameObject healthSupplyShip;
	[SerializeField] private GameObject torpedoSupplyShip;
	
	void Update () {
		supplyHealthDrop--;
		Debug.Log (supplyHealthDrop);
		if (supplyHealthDrop <= 0){
			SpawnHealthSupplyShip();
			supplyHealthDrop = 1725;
		}
		
		supplyTorpedoDrop--;
		//Debug.Log (supplyTorpedoDrop);
		if (supplyTorpedoDrop <= 0){
			SpawnTorpedoSupplyShip();
			supplyTorpedoDrop = 3000;
		}		
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
	}
	
	private void SpawnHealthSupplyShip(){
		print("Spawn health");
		foreach(Transform child in transform){
			GameObject healthShip = Instantiate(healthSupplyShip, child.transform.position, Quaternion.identity)as GameObject;
			healthShip.transform.parent = child;
		}
	}
	private void SpawnTorpedoSupplyShip(){
		foreach(Transform child in transform){
			GameObject torpedoShip = Instantiate(torpedoSupplyShip, child.transform.position, Quaternion.identity)as GameObject;
			torpedoShip.transform.parent = child;
		}
	}
}


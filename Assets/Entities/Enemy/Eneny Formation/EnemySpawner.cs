using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	
	public static int enemy1Count = 36;
	public static int enemy2Count = 27;
	public static int enemy3Count = 18;

	[SerializeField] private GameObject enemy1;
	[SerializeField] private GameObject enemy2;
	[SerializeField] private GameObject enemy3;

	[SerializeField] private float width = 10f;
	[SerializeField] private float height = 5f;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float spawnDelay = 0.5f;
	
	private bool movingRight = true;
	private float minX;
	private float maxX;

	void Start (){
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		minX = leftBoundary.x;
		maxX = rightBoundary.x;
		//SpawnEnemy();
		SpawnUntilFull();
	}
	
	void Update (){
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);	
		if(leftEdgeOfFormation < minX){
			movingRight = true;
		}
		else if (rightEdgeOfFormation > maxX){
			movingRight = false;
		}
		if(AllMembersDead()){
			if ((EnemySpawner.enemy1Count <= 0) && (EnemySpawner.enemy2Count <= 0) && (EnemySpawner.enemy3Count <= 0)){
				LevelManager man = GameObject.Find("Level Manager").GetComponent<LevelManager>();
				man.LoadLevel("Win Screen");
			}
			else{
				SpawnUntilFull();
			}
		}
	}
	
	//void SpawnEnemy(){
	//	foreach(Transform child in transform){
	//		GameObject enemy = Instantiate(enemy1, child.transform.position, Quaternion.identity)as GameObject;
	//		enemy.transform.parent = child;
	//	}
	//}

	private void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
			if(enemy1Count > 0){
				InstantiateEnemy(enemy1, freePosition);
				//enemy.transform.parent = freePosition;
			}
			else if((enemy1Count <= 0) && (enemy2Count > 0)) {
				InstantiateEnemy(enemy2, freePosition);
			}
			else if((enemy1Count <= 0) && (enemy2Count <= 0)){
				InstantiateEnemy(enemy3, freePosition);
			}		
		}
		if(NextFreePosition()){
		Invoke("SpawnUntilFull", spawnDelay);
		
		}
	}

	private void InstantiateEnemy(GameObject enemyGO, Transform freePosition){
		GameObject enemy = Instantiate(enemyGO, freePosition.position, Quaternion.identity) as GameObject;
		enemy.transform.SetParent(freePosition);
	}

	private Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount == 0){
				return childPositionGameObject;
				}
			}
			return null;
	}
	
	private bool AllMembersDead (){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
}

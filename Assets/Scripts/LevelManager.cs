using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel (string name){
		Debug.Log("Level load requested for: " +name);
		SceneManager.LoadScene(name);
		Reset();
	}
	
	public void QuitRequest () {
		Debug.Log("Level Quit Request");
		Application.Quit();
	}
	
	public void LoadNextLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Reset();
	}
	private void Reset(){
		EnemySpawner.enemy1Count = 36;
		EnemySpawner.enemy2Count = 27;
		EnemySpawner.enemy3Count = 18;
		SupplyDrop.supplyHealthDrop = 1725;
		SupplyDrop.supplyTorpedoDrop = 3000;
	}
}

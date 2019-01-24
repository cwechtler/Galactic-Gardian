using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	[SerializeField] private AudioClip startClip;
	[SerializeField] private AudioClip gameClip;
	[SerializeField] private AudioClip winClip;
	[SerializeField] private AudioClip endClip;
	
	private AudioSource music;
	
	void Awake (){
		if (instance !=null && instance !=this) {
			Destroy (gameObject);
		}else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();			
		}	
	}
	
	void OnLevelWasLoaded(int level){
		Debug.Log ("MusicPlayer: loaded level "+level);
		music.Stop();	
		if (level == 0){
			music.clip = startClip;
		}
		if(level == 1){	
			music.clip = gameClip; 
		}
		if(level == 2){
			music.clip = winClip; 
		}
		if(level == 3){
			music.clip = endClip; 
		}
		if(level == 4){	
			music.clip = startClip;
		}
		
		music.loop = true;	
		music.Play();
	}				
}

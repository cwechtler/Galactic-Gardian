using UnityEngine;
using UnityEngine.UI;

public class ShieldActivation : MonoBehaviour {

	[SerializeField] private Image currentEnergybar;
	[SerializeField] private Text energyRatioText;
	[SerializeField] private GameObject shield;
	[SerializeField] private GameObject player;
	[SerializeField] private float energypoints = 50f;

	private float maxEnergypoints = 50f;
	
	
	void Update(){
		if (Input.GetKey(KeyCode.LeftAlt)){
			ShieldGenerator();
		}
		if(Input.GetKeyUp(KeyCode.LeftAlt)){
			foreach (Transform child in transform){
				Destroy(child.gameObject);
			}
		}
		if(energypoints <= 0){		
			foreach (Transform child in transform){
				Destroy(child.gameObject);
			}
		}		
		UpdateEnergybar();		
	}
	
	private void ShieldGenerator(){	
		GameObject Shield = Instantiate(shield, transform.position, Quaternion.identity) as GameObject;
		Shield.transform.SetParent(this.transform);
		energypoints -= Time.fixedDeltaTime; //* 20;
		if (energypoints < 0){
			energypoints = 0;
		}
	}
	
	private void UpdateEnergybar() {
		float ratio = energypoints / maxEnergypoints;
		currentEnergybar.rectTransform.localScale = new Vector3(1,ratio,1);
		energyRatioText.text = (ratio*100).ToString() + '%';
	}	
}

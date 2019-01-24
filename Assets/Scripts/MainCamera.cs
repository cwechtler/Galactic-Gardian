using UnityEngine;

public class MainCamera : MonoBehaviour {

	[SerializeField] private float width = 5f;
	[SerializeField] private float height = 5f;

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
}

using UnityEngine;
using System.Collections;

public class gravity_sphere : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay (Collider other) {
		float gravitational_constant = 0.002f;
		float distance = this.transform.localScale.x - Vector3.Distance (other.gameObject.transform.position, this.transform.position);
		other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, this.transform.position, gravitational_constant * distance / other.rigidbody.mass);
	}
}

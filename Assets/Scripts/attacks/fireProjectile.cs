//Reference: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html

using UnityEngine;
using System.Collections;

/**
* Spawn a rigid body GameObject with an initial velocity when triggered. 
* Constraints: The projectile must contain a rigid body.
*/
public class fireProjectile: MonoBehaviour {
	public GameObject projectile;
	public Vector3 offset;
	public Vector3 trajectory = Vector3.forward;
	public float power = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			GameObject clone;
			clone = Instantiate(projectile, transform.position + offset, transform.rotation) as GameObject;
			clone.rigidbody.velocity = transform.TransformDirection(trajectory * power);
		}
	}
}

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

	public enum TrajectoryType {Straight, Lob};
	public TrajectoryType style = TrajectoryType.Straight;
	public float power = 50;
	public float lobDampener = 5;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftShift)){
			trajectory = Vector3.forward/10;
			style = TrajectoryType.Straight;
			projectile.GetComponent<CollisionForce>().forceType = CollisionForce.ForceType.Pull;
			Fire();
		}
		else if(Input.GetKeyDown(KeyCode.LeftControl)){
			trajectory = (Vector3.forward / Mathf.Max(0, lobDampener) ) + (Vector3.up / Mathf.Max(0, lobDampener) );
			style = TrajectoryType.Lob;
			projectile.GetComponent<CollisionForce>().forceType = CollisionForce.ForceType.Pull;
			Fire();
		}
		else if (Input.GetButtonDown("Fire1")){
			trajectory = Vector3.forward;
			style = TrajectoryType.Straight;
			projectile.GetComponent<CollisionForce>().forceType = CollisionForce.ForceType.Push;
			Fire();
		}
		else if(Input.GetButtonDown("Fire2")){
			trajectory = (Vector3.forward / Mathf.Max(0, lobDampener) ) + (Vector3.up / Mathf.Max(0, lobDampener) );
			style = TrajectoryType.Lob;
			projectile.GetComponent<CollisionForce>().forceType = CollisionForce.ForceType.Push;
			Fire();

		}
	}

	void Fire(){
		GameObject clone;
		clone = Instantiate(projectile, transform.position + offset, transform.rotation) as GameObject;
		clone.rigidbody.velocity = transform.TransformDirection(trajectory * power);
	}
}

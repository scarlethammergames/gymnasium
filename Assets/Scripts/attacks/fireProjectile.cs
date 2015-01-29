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
	public float magnitude = 50;
	public float drag = 5;
	public bool makeChild = false;

	public string inputName = "Fire1";

//	public enum TrajectoryType {Straight, Lob, Drop, Attach};
//	public TrajectoryType style = TrajectoryType.Straight;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButtonDown( inputName ) ){
			Fire();
		}
	}

	void Fire(){
		GameObject clone;
		clone = Instantiate( projectile, transform.position + offset, transform.rotation ) as GameObject;
		clone.rigidbody.velocity = transform.TransformDirection( trajectory * magnitude );
		if( makeChild ){
			clone.transform.parent = this.transform;
		}
	}
}

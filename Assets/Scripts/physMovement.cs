using UnityEngine;
using System.Collections;

/**
 * This is a basic controller for moving a rigid-body object.
 * Requires a 'Constant Force' component.
 */
public class physMovement : MonoBehaviour {
	public float magnitude = 5f;
	Vector3 direction = new Vector3();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		direction.Set(0,0,0);
		if(Input.GetKey(KeyCode.LeftArrow))
		{
		   direction.x = -magnitude;
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
		   direction.x = magnitude;
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
		   direction.z = magnitude;		
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
		   direction.z = -magnitude;
		}
		
		constantForce.force = direction;
	}
}

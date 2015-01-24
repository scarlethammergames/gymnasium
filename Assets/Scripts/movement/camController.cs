using UnityEngine;
using System.Collections;

public class camController : MonoBehaviour {
	public float magnitude = 90f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		#region CamRotations
		if(Input.GetKey(KeyCode.J))
		{
			transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * -magnitude);
		}
		if(Input.GetKey(KeyCode.L))
		{
			transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * magnitude);
		}
		if(Input.GetKey(KeyCode.I))
		{
			transform.RotateAround(transform.position, transform.right, Time.deltaTime * magnitude);		
		}
		if(Input.GetKey(KeyCode.K))
		{
			transform.RotateAround(transform.position, transform.right, Time.deltaTime * -magnitude);
		}	
		#endregion CamRotations
	}
}

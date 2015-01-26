using UnityEngine;
using System.Collections;

public class Hookshot : MonoBehaviour {
	
	public float PlayerMoveSpeed;
	private Vector3 TargetPosition;
	public bool isMoving;
	
	// Use this for initialization
	void Start () {
		isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (( System.Math.Abs(TargetPosition.x - transform.position.x) < 1.5) && (System.Math.Abs(TargetPosition.z - transform.position.z)< 1.5)) {
			isMoving=false;
		}
		if (Input.GetButtonDown("Hookshot")) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				isMoving = true;
				TargetPosition = hit.point;
				Debug.Log ("hit " + hit.point);
				Debug.DrawLine(ray.origin, hit.point);
//				if (TargetPosition.y < transform.position.y) {
//					TargetPosition.y = transform.position.y;
//				}
			}
		}
		if (isMoving==true) {
			transform.position = Vector3.Lerp (transform.position, TargetPosition, Time.deltaTime*PlayerMoveSpeed);
		}
	}
}

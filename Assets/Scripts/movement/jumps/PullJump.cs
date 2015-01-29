using UnityEngine;
using System.Collections;

public class PullJump : MonoBehaviour {
	
	public float pullPower=3;
	public int grappleRange = 50;
	private Vector3 TargetPosition;
	private bool isMoving;
	private GameObject camera;
	private Crosshair crosshair;

	
	// Use this for initialization
	void Start () {
		isMoving = false;
		camera = GameObject.Find("FreeLookCameraRig");
		crosshair = camera.GetComponent<Crosshair>();
	}
	
	// Update is called once per frame
	void Update () {
		//If player is within certain distance of grappled location, stop being "pulled" towards it
		if (( System.Math.Abs(TargetPosition.x - transform.position.x) < 1.5) && (System.Math.Abs(TargetPosition.z - transform.position.z)< 1.5)) {
			isMoving=false;

		}
		//Detects if player grapples
		if (Input.GetMouseButtonDown (1)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, grappleRange)) {
				isMoving = true;
				TargetPosition = hit.point;
//				Debug.Log ("hit " + hit.point);
				Debug.DrawLine(ray.origin, hit.point);
			} else {
				//Grapping something that is out of our range
				//Start the invalid crosshair effect
				crosshair.failAim = true;
			}
		}
		if (isMoving==true) {
			//Move player
			transform.position = Vector3.Lerp (transform.position, TargetPosition, Time.deltaTime*pullPower);
			//Make crosshair pulse 
			crosshair.spread += (crosshair.spreadPerSecond * Time.deltaTime);
		} else {
			//Shrink crosshair back to normal
			crosshair.spread -= crosshair.spreadPerSecond * 3 * Time.deltaTime;   
		}
	}
}

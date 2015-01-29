using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets.Characters.ThirdPerson_Mod
{
	public class BurstJump : MonoBehaviour {

		public float power = 20.0f;
		public float burstDownThreshold = 5.0f;
		private Vector3 move;
		private Vector3 camForward;
		private Transform cam;
		private Vector3 lookPos;
		private ThirdPersonCharacter_Mod character;
		private bool burst;
		private bool burstHover;

		// Use this for initialization
		void Start () {
			character = GetComponent<ThirdPersonCharacter_Mod>();
			burst = false;

			if (Camera.main != null)
			{
				cam = Camera.main.transform;
			}
			else
			{
				Debug.LogWarning(
					"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
				// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
			}
		}
		
		// Update is called once per frame
		void Update () {
			//If in the air, and around apex, middle click to burst down
			if (burst && rigidbody.velocity.y < burstDownThreshold && Input.GetMouseButtonDown(0)) {
				Debug.Log ("Bursting down");
				character.burstInput = -1.0f;
				burst = false;
				executeMove();
				
			} else
			if (Input.GetMouseButtonDown (1)) {
				//Commented out code below doesn't work because of Ethan's ThirdPersonCharacter.cs that overrides it
				//rigidbody.velocity = (transform.up + transform.forward) * power;
				executeMove();
				burst = true;
				character.burstInput = power;
				executeMove();
				character.burstInput = 1.0f;
			}
			 
		}
		void executeMove() {
			camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");
			move = (v*camForward + h*cam.right);
			move = move * power;
			// calculate the head look target position
			lookPos = cam != null
				? transform.position + cam.forward*100
					: transform.position + transform.forward*100;
			character.Move (move, false, true, lookPos);
		}
	}
}

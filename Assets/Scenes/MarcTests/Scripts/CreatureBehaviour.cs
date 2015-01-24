using UnityEngine;
using System.Collections;

public class CreatureBehaviour : MonoBehaviour {

	Animator anim;
	Rigidbody rbody;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Vertical") > 0){
			anim.speed = 1;
			anim.SetBool("isIdle",false);
			anim.SetBool("isWalking",true);
		}
		else if(Input.GetAxisRaw("Vertical") < 0){
			anim.speed = -1;
			anim.SetBool("isIdle",false);
			anim.SetBool("isWalking",true);
		}
		else if(Input.GetKeyDown(KeyCode.Space)){
			anim.SetTrigger("isJumping");
			rigidbody.AddForce(transform.up * 10, ForceMode.Impulse);
		}
		else{
			anim.speed = 1;
			anim.SetBool("isWalking",false);
			anim.SetBool("isIdle",true);
		}
	}


}

using UnityEngine;
using System.Collections;

public class CollisionForce : MonoBehaviour {
	public float magnitude;
	public float radius;

	public int lifespan = 100;
	private int life;
	private float radiusLife;
	private float magnitudeLife;

	public enum ForceType {Push, Pull};
	public ForceType forceType = ForceType.Push;


	// Use this for initialization
	void Start () {
		life = lifespan;
		radiusLife = radius;
		magnitudeLife = magnitude;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(forceType == ForceType.Push){
			Shockwave();
		}
		else{
			Blackhole();
		}
	}

	void Blackhole(){
		if(life > 0){
			Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
			foreach (Collider hit in hitColliders)
			{
				if (hit && hit.rigidbody){
					ForceConditions fc = (ForceConditions) hit.GetComponent(typeof(ForceConditions));
					if(fc.canPull()){
						hit.rigidbody.AddForce(( this.transform.position - hit.transform.position)*magnitude, ForceMode.Impulse);
					}
				}
			}
			life--;
			if(radiusLife > 0){
				radiusLife--;
				if(magnitudeLife > 0){
					magnitudeLife--;
				}
			}
		}
	}

/// <summary>
/// Create a shockwave.
/// </summary>

	void Shockwave(){
		if(life > 0){
			Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
			foreach (Collider hit in hitColliders)
			{
				if (hit && hit.rigidbody){
					ForceConditions fc = (ForceConditions) hit.GetComponent(typeof(ForceConditions));
					if(fc.canPush()){
						hit.rigidbody.AddForce(Vector3.up * magnitude, ForceMode.Impulse);
						hit.GetComponent<ForceConditions>().pullable = true;
					}
				}
			}
			life--;
			if(radiusLife > 0){
				radiusLife--;
				if(magnitudeLife > 0){
					magnitudeLife--;
				}
			}
		}
	}
}

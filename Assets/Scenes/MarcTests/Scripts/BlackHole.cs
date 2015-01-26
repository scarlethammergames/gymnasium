using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {
	public float magnitude;
	public float radius;
	
	public int lifespan = 1000;
	private int life;
	private float radiusLife;
	private float magnitudeLife;

	public float targetScale = 0.1f;
	public float shrinkSpeed = 1.0f;
	
	// Use this for initialization
	void Start () {
		life = lifespan;
		radiusLife = radius;
		magnitudeLife = magnitude;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Blackhole();
	}
	
	void Blackhole(){
		if(life > 0){
			Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
			foreach (Collider hit in hitColliders)
			{
				if (hit && hit.rigidbody){
					ForceConditions fc = (ForceConditions) hit.GetComponent(typeof(ForceConditions));

					if(fc && fc.canPull()){
						hit.rigidbody.AddForce(( this.transform.position - hit.transform.position )*magnitude, ForceMode.Impulse);
//						hit.GetComponent<SetRenderQueue>().SetQueueVal(0,2998);
//						hit.GetComponent<SetRenderQueue>().SetRenderQueue( 0, 2998 );

						hit.transform.localScale = Vector3.Lerp(hit.transform.localScale, Vector3.one * targetScale, shrinkSpeed*Time.deltaTime);
						if(hit.transform.localScale.x <= targetScale){
							hit.gameObject.SetActive( false );
						}
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

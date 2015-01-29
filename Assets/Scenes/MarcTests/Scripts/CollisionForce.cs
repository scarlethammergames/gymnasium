using UnityEngine;
using System.Collections;

public class CollisionForce : MonoBehaviour {
	public float _magnitudeBirth = 100;
	public float _magnitudeDeath = 0;
	public float _magRatio = 1;
	public float _radiusBirth = 100;
	public float _radiusDeath = 0;
	public float _radiusRatio = 1;
	public int _maxDuration = 100;

	public enum ForceType {Push, Pull, Lift};
	public ForceType _forceType = ForceType.Push;
	
	private int _duration;
	private float _currentRadius;
	private float _currentMagnitude;
	private int _maxMagnitude = 1000000;

	public bool debug = true;


	// Use this for initialization
	void Start () {
		_currentMagnitude = _magnitudeBirth;
		_currentRadius = _radiusBirth;
		_duration = _maxDuration;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( _duration > 0 ){
			_duration--;
			_currentRadius = Mathf.Lerp( _magnitudeBirth, _magnitudeDeath, _radiusRatio * Time.deltaTime );
			_currentMagnitude = Mathf.Lerp( _radiusBirth, _radiusDeath, _radiusRatio * Time.deltaTime );
			ForceSphere( _currentRadius, _currentMagnitude, _forceType );
		}
	}

	/// <summary>
	/// Apply a force to any object with a collider that exists within the sphere.
	/// Object to be affected must be in the "PushPull" layer.
	/// </summary>
	/// <param name="radius">Radius of influence.</param>
	/// <param name="magnitude">Magnitude of pulling force.</param>
	/// <param name="forceType">Type of force affects the direction force is applied.</param>
	void ForceSphere( float radius, float magnitude, ForceType forceType )
	{
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
		foreach (Collider hit in hitColliders)
		{
			if (hit && hit.rigidbody){
				ForceConditions fc = (ForceConditions) hit.GetComponent(typeof(ForceConditions));
				if( fc ){
					switch( forceType ){
					case ForceType.Push:
						if( fc.canPush() ){
							Vector3 direction = Vector3.Normalize( hit.transform.position - this.transform.position );
							hit.rigidbody.AddForce( Vector3.ClampMagnitude( (magnitude/(direction.sqrMagnitude))*direction, _maxMagnitude ) , ForceMode.Impulse);
							hit.GetComponent<ForceConditions>().setPullable(true);
						}				
						break;
					case ForceType.Pull:
						if( fc.canPull() ){
							Vector3 direction = Vector3.Normalize( hit.transform.position - this.transform.position );
							hit.rigidbody.AddForce( Vector3.ClampMagnitude( -1 * (magnitude)*direction, _maxMagnitude ) , ForceMode.Impulse);
						}		
						break;
					case ForceType.Lift:
						if( fc.canPush() ){
							hit.rigidbody.AddForce( Vector3.up * Mathf.Clamp(magnitude/Vector3.Magnitude(this.transform.position - hit.transform.position), 0, _maxMagnitude) , ForceMode.Impulse);
						}
						break;
					}
				}
			}
		}
	}

	/// <summary>
	/// Raises the draw gizmos selected event.
	/// </summary>
	void OnDrawGizmosSelected() {
		//Debugging
		if(debug){
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position, _currentRadius);
		}
	}
}

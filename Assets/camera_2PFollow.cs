using UnityEngine;
using System.Collections;

public class camera_2PFollow : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	public float smoothing = 1.0f;
	public Vector3 offset;
	public Vector3 rotationOffset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player1.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player1.transform.position - player2.transform.position + offset;
	}
}

using UnityEngine;
using System.Collections;

public class SupplyDepotBehaviour : MonoBehaviour {

	private int maxSize;
	private int currentSize;

	// Use this for initialization
	void Start () {

		maxSize = 5000;
		currentSize = 0;
	
	}



	public SupplyDepotBehaviour getInstance() {

		return this;

	}

	int getSize(){

		return this.maxSize;

	}

	internal void updateSize(int item){

		if ((this.currentSize + item) < 5000) {
			this.currentSize += item;
		}else{
			Debug.Log ("Resource Depot FuLL");
		}


	}


	// Update is called once per frame
	void Update () {

		Debug.Log (currentSize);
	
	}
}

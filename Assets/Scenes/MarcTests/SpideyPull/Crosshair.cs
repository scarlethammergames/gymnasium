using UnityEngine;
using System.Collections;

/// <summary>
/// Crosshair.
/// Note:  Shotgun and Crysis presets do not work with our game 
/// since we want crosshair to follow the cursor and Monica only
/// modded the none-preset one.
/// </summary>

public class Crosshair : MonoBehaviour {

	public enum preset { none, shotgunPreset, crysisPreset }
	public preset crosshairPreset = preset.none;
	
	public bool showCrosshair = true;
	public float invalidAimTime = 1.0f;

	public Texture2D verticalTexture ;
	public Texture2D horizontalTexture ;
	public Texture2D invalidTexture ;
	
	//Size of boxes
	public float cLength = 10.0f;
	public float cWidth = 3.0f;
	
	//Spreed setup
	public float minSpread = 45.0f;
	public float maxSpread = 250.0f;
	public float spreadPerSecond = 150.0f;
	
	//Rotation
	public float rotAngle = 0.0f;
	public float rotSpeed = 0.0f;
	
	[HideInInspector] public Texture2D temp;
	[HideInInspector] public float spread;
	[HideInInspector] public bool failAim;

	//Variables for invalid crosshair
	private double timer;
	private float lastFail;//the time since the last invalid aim
	private bool blink;
	void Start ()
	{
		crosshairPreset = preset.none;
		failAim = false;
		lastFail = 0.0f;
		timer = 0;
		blink = false;
	}
	
	void Update()
	{
		//Used just for test (weapon script should change spread).
//		if(Input.GetKey(KeyCode.K)) spread += spreadPerSecond * Time.deltaTime;
//		else spread -= spreadPerSecond * 2 * Time.deltaTime;   
		
		//Rotation
		rotAngle += rotSpeed * Time.deltaTime;
	}
	
	void OnGUI() {
		Vector3 mousePosition = Input.mousePosition;
//		Debug.Log ("Cursor: " + mousePosition);

		if(showCrosshair && verticalTexture && horizontalTexture){
			GUIStyle verticalT = new GUIStyle();
			GUIStyle horizontalT = new GUIStyle();
			GUIStyle invalidT = new GUIStyle();
			verticalT.normal.background = verticalTexture;
			horizontalT.normal.background = horizontalTexture;
			invalidT.normal.background = invalidTexture;

			spread = Mathf.Clamp(spread, minSpread, maxSpread);
			Vector2 pivot = new Vector2(mousePosition.x, Screen.height -mousePosition.y);
			
			if(crosshairPreset == preset.crysisPreset){
				
				GUI.Box(new Rect((Screen.width - 2)/2, (Screen.height - spread)/2 - 14, 2, 14), temp, horizontalT);
				GUIUtility.RotateAroundPivot(45,pivot);
				GUI.Box(new Rect((Screen.width + spread)/2, (Screen.height - 2)/2, 14, 2), temp, verticalT);
				GUIUtility.RotateAroundPivot(0,pivot);
				GUI.Box(new Rect((Screen.width - 2)/2, (Screen.height + spread)/2, 2, 14), temp, horizontalT);
			}
			
			if(crosshairPreset == preset.shotgunPreset){
				
				GUIUtility.RotateAroundPivot(45,pivot);
				
				//Horizontal
				GUI.Box(new Rect((mousePosition.x - 14), (Screen.height - mousePosition.y) - 3, 14, 3), temp, horizontalT);
				GUI.Box(new Rect((mousePosition.x - 14), (Screen.height + mousePosition.y), 14, 3), temp, horizontalT);
				//Vertical
				GUI.Box(new Rect((mousePosition.x  - spread) - 3, (mousePosition.y - 14), 3, 14), temp, verticalT);
				GUI.Box(new Rect((mousePosition.x  + spread), (mousePosition.y - 14), 3, 14), temp, verticalT);
			}
			
			if(crosshairPreset == preset.none && failAim==false){
				GUIUtility.RotateAroundPivot(rotAngle%360,pivot);

				GUI.Box(new Rect(mousePosition.x - cWidth, (Screen.height- mousePosition.y)- spread- cLength, cWidth, cLength), temp, horizontalT);
				GUI.Box(new Rect(mousePosition.x - cWidth, (Screen.height- mousePosition.y) + spread, cWidth, cLength), temp, horizontalT);
				//Vertical (left and right lines)
				GUI.Box(new Rect(mousePosition.x- spread - cLength, (Screen.height- mousePosition.y) - cWidth, cLength, cWidth), temp, verticalT);
				GUI.Box(new Rect(mousePosition.x + spread, (Screen.height- mousePosition.y)- cWidth, cLength, cWidth), temp, verticalT);
			} else if (crosshairPreset == preset.none && failAim==true) {
				lastFail += Time.deltaTime;
				if (lastFail > invalidAimTime) {
					lastFail = 0.0f;
					failAim = false;
				} else {
					GUIUtility.RotateAroundPivot(rotAngle%360,pivot);
					//Paint crosshair the invalid texture color
					if (blinkingCrosshair()) {
						//Horizontal (top and bottom lines)
						float invalidWidth = cWidth*3;
						GUI.Box(new Rect(mousePosition.x - cWidth, (Screen.height- mousePosition.y)- spread- cLength, invalidWidth, cLength), temp, invalidT);
						GUI.Box(new Rect(mousePosition.x - cWidth, (Screen.height- mousePosition.y) + spread, invalidWidth, cLength), temp, invalidT);
						//Vertical (left and right lines)
						GUI.Box(new Rect(mousePosition.x- spread - cLength, (Screen.height- mousePosition.y) - cWidth, cLength, invalidWidth), temp, invalidT);
						GUI.Box(new Rect(mousePosition.x + spread, (Screen.height- mousePosition.y)- cWidth, cLength, invalidWidth), temp, invalidT);
					}
				}
			}
		}
	}
	bool blinkingCrosshair() {
		if (Time.time > timer) {
			timer = Time.time + 0.1;
			blink = !blink;
		}
		return blink;
	}
}

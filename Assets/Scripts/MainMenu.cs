using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

  public GUISkin skin;

  Vector2 scrollPos = new Vector2();
  string description = "";

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void onGUI()
  {
    GUI.skin = this.skin;
    GUILayout.Space(10);

    GUILayout.BeginHorizontal();
    GUILayout.Space(10);
    scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(400));
  }
}

using UnityEngine;
using System.Collections;

public class DeployEMP : MonoBehaviour
{

  public GameObject empPrefab;

  GameObject player;
  float lastFired;
  Transform transform;

  // Use this for initialization
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
  }

  // Update is called once per frame
  void Update()
  {
    lastFired += Time.deltaTime;
    transform = player.transform;
    if (Input.GetKey(KeyCode.H))
    {
      if (lastFired > 5.0f)
      {
        GameObject emp = (GameObject)Instantiate(empPrefab, transform.position + new Vector3(0f, 2f, 0f), transform.rotation);
        lastFired = 0.0f;
      }
    }
  }
}

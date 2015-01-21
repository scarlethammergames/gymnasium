using UnityEngine;
using System.Collections;

public class DeploySingularity : MonoBehaviour
{

  public GameObject singularityPrefab;
  public float speed = 20f;
  public float fireRate = .5f;

  GameObject player;
  Transform transform;
  float lastFired;
  bool charging;
  GameObject heldSingularity;

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
  }

  void Update()
  {
    lastFired += Time.deltaTime;
    if (charging)
    {
      if (Input.GetMouseButtonUp(0))
      {
        if(lastFired < 5.0)
        {
          gameObject.GetComponent("");
        }
      }
    }
    else
    {
      transform = player.transform;
      if (Input.GetMouseButton(0))
      {
        if (lastFired > fireRate)
        {
          lastFired = 0.0f;
          GameObject singularity = (GameObject)Instantiate(singularityPrefab, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z), transform.rotation);
          heldSingularity = singularity;
        }
      }
    }
  }
}

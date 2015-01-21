using UnityEngine;
using System.Collections;

public class SingularityBehaviour : MonoBehaviour
{

  public float singularityPower = 1.0f; // should range from 0 to 1.0
  public float singularityBlownRadiusMultiplier = 50.0f;
  public float singularityTravellingRadiusMultiplier = .1f;
  public float singularityBlownTimeMultiplier = 5.0f;
  public float gravitational_constant = 0.02f;
  public bool blowOnContact = true;
  public float speed = 10f;

  float time;
  float blowTime;
  bool blown;

  // Use this for initialization
  void Start()
  {
    Debug.Log("Starting");
    time = 0.0f;
    blown = false;
    blowTime = singularityPower * singularityBlownTimeMultiplier;
    this.transform.localScale = this.transform.localScale * singularityTravellingRadiusMultiplier * singularityPower;
  }

  void Blow()
  {
    this.transform.localScale = this.transform.localScale * singularityBlownRadiusMultiplier * singularityPower;
    blown = true;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    time += Time.deltaTime;
    if (!blown)
    {
      this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, this.gameObject.transform.position + this.gameObject.transform.forward * speed, Time.deltaTime);
      if (time > blowTime)
      {
        Blow();
        time = 0.0f;
      }
    }
    else
    {
      if (time > blowTime)
      {
        Destroy(this.gameObject);
      }
    }
  }

  void OnTriggerStay(Collider other)
  {
    if (other.rigidbody != null)
    {
      if(other.CompareTag("Player"))
      {
        return;
      }
      float distance = Vector3.Distance(other.gameObject.transform.position, this.transform.position);
      Debug.Log(distance);
      other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, this.transform.position, gravitational_constant * distance / other.rigidbody.mass);
      if (distance < 2)
      {
        if (blown == false)
        {
          if (blowOnContact)
          {
            Blow();
          }
        }
        Destroy(other.gameObject);
      }
    }
  }
}

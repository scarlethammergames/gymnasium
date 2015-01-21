using UnityEngine;
using System.Collections;

public class EMPBehaviour : MonoBehaviour
{

  public float maxSpinSpeed = 360;
  public float explosionForce = 10.0f;
  public float explosionRadius = 50.0f;
  public float explosionTime = 3.0f;
  public float flareStrengthDivider = 3.0f;
  public float cooldownFlareStrengthDivider = 6.0f;
  public float cooldownMultiplier = 2.0f;

  bool activated = false;
  bool cooling = false;
  float chargingTimer = 0.0f;
  LensFlare lensFlare;
  float cooldownTime;

  void Start()
  {
    lensFlare = this.gameObject.GetComponent<LensFlare>();
    lensFlare.brightness = 0.0f;
    cooldownTime = explosionTime * cooldownMultiplier;
  }

  void add_emp(GameObject obj, float radius)
  {
  }

  void FixedUpdate()
  {
    if (Input.GetKey(KeyCode.J))
    {
      activated = true;
    }
    if (activated)
    {
      chargingTimer += Time.deltaTime;
      if (chargingTimer < explosionTime)
      {
        this.gameObject.transform.Rotate(Vector3.up, maxSpinSpeed * Time.deltaTime * chargingTimer);
        lensFlare.brightness = chargingTimer / flareStrengthDivider;
      }
      else
      {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
          if (hit && hit.rigidbody)
            hit.rigidbody.AddExplosionForce(explosionForce, this.transform.position, explosionRadius, 3.0F);
        }
        activated = false;
        chargingTimer = 0.0f;
        lensFlare.brightness = 0.0f;
        cooling = true;
      }
    }
    if (cooling)
    {
      chargingTimer += Time.deltaTime;
      if(chargingTimer < explosionTime * cooldownMultiplier)
      {
        this.gameObject.transform.Rotate(Vector3.up, maxSpinSpeed * Time.deltaTime * (cooldownTime - chargingTimer) / cooldownMultiplier);
        lensFlare.brightness = (cooldownTime - chargingTimer) / cooldownFlareStrengthDivider ;
      }
      else
      {
        cooling = false;
        chargingTimer = 0.0f;
      }
    }
  }
}

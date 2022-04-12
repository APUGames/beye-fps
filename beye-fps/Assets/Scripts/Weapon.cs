using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fPCamera;
    [SerializeField] float range = 100f;

    //gun damage
    [SerializeField] float damage = 3.0f;

    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] GameObject hitEffect;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        ProcessRaycast();
        PlayMuzzleFlash();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(fPCamera.transform.position, fPCamera.transform.forward, out hit, range))
        {
            print("I hit this: " + hit.transform.name);
            // Hit effect for visual feedback
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }
    private void PlayMuzzleFlash()
        {
        muzzleFlash.Play();
        }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1.0f);
    }
}

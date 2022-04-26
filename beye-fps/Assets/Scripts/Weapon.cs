using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fPCamera;

    [SerializeField] float range = 100f;
    //gun damage
    [SerializeField] float damage = 3.0f;

    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] GameObject hitEffect;

    [SerializeField] Ammo ammoSlot;

    [SerializeField] AmmoType ammoType;

    [SerializeField] float timeBetweenShots = 0.5f;

    [SerializeField] TextMeshProUGUI ammoText;

    AudioSource audio;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }


    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();

        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ProcessRaycast();
            PlayMuzzleFlash();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }
    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(fPCamera.transform.position, fPCamera.transform.forward, out hit, range))
        {
            //print("I hit this: " + hit.transform.name);
            // Hit effect for visual feedback
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            
            if (target == null)
            {
                return;
            }

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

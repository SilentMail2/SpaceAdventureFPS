using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    enum clipType { Auto, HighCal, LowCal }
    [SerializeField] clipType thisClipType = clipType.Auto;
    public string GunClipType;

    public float bdam = 1f;
    public float eDam = 1f;
    public float impactForce = 1f;
    public float fireRate = 15f;
    public float range = 100f;
    public int ammo = 10;
    public int maxAmmo = 10;
    public int clip = 10;
    public float spread;
    public Slider ammoIndicator;
    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;
    private float nextTimeToFire = 0f;
    public bool Auto = false;
    // Update is called once per frame
    private void Awake()
    {
        GunClipType = thisClipType.ToString();
        ammoIndicator.maxValue = maxAmmo;
        ammoIndicator.value = ammo;
        fpsCam = Camera.main;
    }
    void Update()
    {
        if (ammo > 0)
        {
            if (Auto)
            {
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Debug.Log("NextFire in " + nextTimeToFire);
                    Shoot();
                }
            }
            if (!Auto)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Debug.Log("NextFire in " + nextTimeToFire + "Current time is " + Time.time);
                    Shoot();
                }
            }
        }

        CheckandRefillAmmo();
    }
    public void Shoot()
    {
        ammo--;
        MuzzleFlash.Play();
        RaycastHit hit;
        Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.gameObject.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeHealth(bdam);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
          GameObject impactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
    public void PickupClip(AmmoPickUp clipPickup)
    {
        clip += clipPickup.clipAmmount;
        Destroy(clipPickup.gameObject);
    }
    public void CheckandRefillAmmo()
    {
        ammoIndicator.value = ammo;
        if (clip >= 0)
        {

            if (ammo < maxAmmo)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ammo = maxAmmo;
                    clip--;
                }
            }
            if (ammo <= 0)
            {
                ammo = maxAmmo;
                clip--;
            }
        }
    }
}

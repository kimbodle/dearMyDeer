using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera; //ī�޶� ����Ʈ(1��Ī ī�޶�)
    [SerializeField] float range = 100f; //����ĳ������ �󸶳� �ָ����� �� �� �մ���
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    //Ư�� ����ü�� �����ٰ� ���� �� ����
    [SerializeField] GameObject hitEffect;

    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShots = 0.5f;

    //����ϴ� ������ ź�� ����
    [SerializeField] AmmoType ammoType;

    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI gunText;
    [SerializeField] TextMeshProUGUI delayText;

    AudioSource shootSound;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
        gunText.text = gameObject.name;
        delayText.gameObject.SetActive(false);
    }
    void Start()
    {
        shootSound = GetComponent<AudioSource>();
        delayText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();
        //GetButtonDown("Fire1")
        if (Input.GetMouseButtonDown(0)&& canShoot ==true)
        {
           StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        delayText.gameObject.SetActive(true);
        //~Ammo(�� �ѿ� �´� ź��)
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            shootSound.Play();
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
        delayText.gameObject.SetActive(false); // �� �� �ִ� ���°� �Ǹ� delayText ��Ȱ��ȭ
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        //������ ����? -> �� / �ƴϿ� bool
        //����, ����ĳ��Ʈ ��Ʈ ���� ���� ���� ����
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            //Debug.Log("I hit this thing: " + hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            { if (target == null) return; } //�� ����
            target.TakeDamage(damage);
        }
        //�ϴ��̳� �� ���� ������ �� ���� ���� ����
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        //���� ȿ���� Ư�� ���⿡�� ���̱� ���� -> hit.normal
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
    public float GetRange()
    {
        return range;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetTimeBetweenShots()
    {
        return timeBetweenShots;
    }
}

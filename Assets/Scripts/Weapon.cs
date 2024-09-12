using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera; //카메라 포인트(1인칭 카메라)
    [SerializeField] float range = 100f; //레이캐스팅이 얼마나 멀리까지 갈 수 잇는지
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    //특정 생명체를 가졌다가 죽일 수 있음
    [SerializeField] GameObject hitEffect;

    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShots = 0.5f;

    //사용하는 무기의 탄약 종류
    [SerializeField] AmmoType ammoType;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetButtonDown("Fire1")
        if (Input.GetMouseButtonDown(0)&& canShoot ==true)
        {
           StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        //~Ammo(각 총에 맞는 탄약)
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        //뭔가를 쐈냐? -> 예 / 아니요 bool
        //방향, 레이캐스트 히트 정보 저장 변수 범위
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            //Debug.Log("I hit this thing: " + hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            { if (target == null) return; } //널 방지
            target.TakeDamage(damage);
        }
        //하늘이나 빈 곳을 쐈을때 널 참조 에러 방지
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        //명중 효과가 특정 방향에서 보이길 원함 -> hit.normal
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
}

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        //뭔가를 쐈냐? -> 예 / 아니요 bool
        //방향, 레이캐스트 히트 정보 저장 변수 범위
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            Debug.Log("I hit this thing: " + hit.transform.name);
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
}

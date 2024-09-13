using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //월드 안에있는 객체의 트랜스폼 찾기
    [SerializeField] Transform target;
    //적이 유니티 유닛에서 쫓아오기 까지 남은 적과의 거리
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    //내비메시 에이전트
    NavMeshAgent navMeshAgent;
    //인식한 타겟과 적이 얼마나 떨어져 있는지
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    EnemyHealth health;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (health.IsDead())
        {
            //죽은 상태면 EnemyAI를 비활성화
            enabled = false;
            navMeshAgent.enabled = false;
            return;
        }

        //타겟의 위치와 본인의 위치를 계산
        distanceToTarget= Vector3.Distance(target.position, transform.position);
        
        //적이 화가 나든가
        if (isProvoked)
        {
            EngageTarget();
        }
        //플레이거가 반경안에 있던가
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            //에이전트의 목적지가 플레이어의 현위치가 되어야함
        }
    }

    public void OnDamageTaken()
    {
        //피해를 입었는지 안입었는지
        //적의 체력이 감소하거나 그 어떤 변화라도 일어나면, 곧바로 피해량 인식
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        //navmeshagent모듈의 값
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        //사정거리 안에 있을때
        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            Attacktarget();
        }
        
    }
    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void Attacktarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
        //Debug.Log(name + "은 발견했고 파괴하는중이다" + target.name);
    }

    void FaceTarget()
    {
        //뺄셈으로 위치를 알아낸뒤에 표준화로 방향 알아내기
        Vector3 direction = (target.position - transform.position).normalized;

        //Quaternion 회전 방향 결정, LookRotation은 Vector3 변수값 사용. 어느방향을 봐야하는지 알려주는 코드
        //Y에 0을 넣는 이유는 적이 타켓을 찾을때 위 아래로 회전 x 기 위해
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //Slerp(구체보간법). 둡벡터값을 기반으로 자연스럽게 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        //Our rotation_적의 위치(현재 위치), Target rotation_타겟 위치, Speed_속력(포착 시간)
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //현재 위치하는 곳, 반경
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

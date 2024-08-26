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

    //내비메시 에이전트
    NavMeshAgent navMeshAgent;
    //인식한 타겟과 적이 얼마나 떨어져 있는지
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
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

    private void EngageTarget()
    {
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

   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //현재 위치하는 곳, 반경
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

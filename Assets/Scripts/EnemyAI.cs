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

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //타겟의 위치와 본인의 위치를 계산
        distanceToTarget= Vector3.Distance(target.position, transform.position);
        if(distanceToTarget <= chaseRange)
        {
            //에이전트의 목적지가 플레이어의 현위치가 되어야함
            navMeshAgent.SetDestination(target.position);
        }
    }
}

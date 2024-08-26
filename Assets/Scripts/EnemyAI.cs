using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //���� �ȿ��ִ� ��ü�� Ʈ������ ã��
    [SerializeField] Transform target;
    //���� ����Ƽ ���ֿ��� �Ѿƿ��� ���� ���� ������ �Ÿ�
    [SerializeField] float chaseRange = 5f;

    //����޽� ������Ʈ
    NavMeshAgent navMeshAgent;
    //�ν��� Ÿ�ٰ� ���� �󸶳� ������ �ִ���
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Ÿ���� ��ġ�� ������ ��ġ�� ���
        distanceToTarget= Vector3.Distance(target.position, transform.position);
        
        //���� ȭ�� ���簡
        if (isProvoked)
        {
            EngageTarget();
        }
        //�÷��̰Ű� �ݰ�ȿ� �ִ���
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            //������Ʈ�� �������� �÷��̾��� ����ġ�� �Ǿ����
        }
    }

    private void EngageTarget()
    {
        //navmeshagent����� ��
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        //�����Ÿ� �ȿ� ������
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
        //Debug.Log(name + "�� �߰��߰� �ı��ϴ����̴�" + target.name);
    }

   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //���� ��ġ�ϴ� ��, �ݰ�
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

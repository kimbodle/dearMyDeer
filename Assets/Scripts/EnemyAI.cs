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

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Ÿ���� ��ġ�� ������ ��ġ�� ���
        distanceToTarget= Vector3.Distance(target.position, transform.position);
        if(distanceToTarget <= chaseRange)
        {
            //������Ʈ�� �������� �÷��̾��� ����ġ�� �Ǿ����
            navMeshAgent.SetDestination(target.position);
        }
    }
}

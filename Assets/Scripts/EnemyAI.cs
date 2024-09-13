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
    [SerializeField] float turnSpeed = 5f;

    //����޽� ������Ʈ
    NavMeshAgent navMeshAgent;
    //�ν��� Ÿ�ٰ� ���� �󸶳� ������ �ִ���
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
            //���� ���¸� EnemyAI�� ��Ȱ��ȭ
            enabled = false;
            navMeshAgent.enabled = false;
            return;
        }

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

    public void OnDamageTaken()
    {
        //���ظ� �Ծ����� ���Ծ�����
        //���� ü���� �����ϰų� �� � ��ȭ�� �Ͼ��, ��ٷ� ���ط� �ν�
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
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

    void FaceTarget()
    {
        //�������� ��ġ�� �˾Ƴ��ڿ� ǥ��ȭ�� ���� �˾Ƴ���
        Vector3 direction = (target.position - transform.position).normalized;

        //Quaternion ȸ�� ���� ����, LookRotation�� Vector3 ������ ���. ��������� �����ϴ��� �˷��ִ� �ڵ�
        //Y�� 0�� �ִ� ������ ���� Ÿ���� ã���� �� �Ʒ��� ȸ�� x �� ����
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //Slerp(��ü������). �Ӻ��Ͱ��� ������� �ڿ������� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        //Our rotation_���� ��ġ(���� ��ġ), Target rotation_Ÿ�� ��ġ, Speed_�ӷ�(���� �ð�)
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //���� ��ġ�ϴ� ��, �ݰ�
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

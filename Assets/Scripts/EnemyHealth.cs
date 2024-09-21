using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    AudioSource deadSound;

    bool isDead = false;


    private void Start()
    {
        deadSound = GetComponent<AudioSource>();
    }
    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage)
    {
        //������ �޾Ƽ� ���ظ� �Ծ��� �� �װ��� �����ϴ� ��ũ��Ʈ�� �������� �������ߵ� =>���� ���� �� � ��ũ��Ʈ�� �ν��� �� �ְ�
        //GetComponent<EnemyAI>().OnDamageTaken();
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if(hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(isDead) { return; }
        isDead = true;
        deadSound.Play();
        GetComponent<Animator>().SetTrigger("die");
        
    }
}

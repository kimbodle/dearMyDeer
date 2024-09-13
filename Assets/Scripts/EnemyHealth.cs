using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    bool isDead = false;


    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage)
    {
        //공격을 받아서 피해를 입었을 때 그것을 인지하는 스크립트가 무엇인지 명시해줘야됨 =>공격 받을 때 어떤 스크립트든 인식할 수 있게
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
        GetComponent<Animator>().SetTrigger("die");
        
    }
}

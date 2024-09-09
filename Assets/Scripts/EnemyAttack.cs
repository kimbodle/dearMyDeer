using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //[SerializeField] Transform target; 비효율적 코드
    PlayerHealth target;
    [SerializeField] float damage = 40f;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }
    public void OnDamageTaken()
    {
        Debug.Log(name + "I also know that we took damage");
    }

    public void AttacjHitEvent()
    {
        if(target ==null) return;
        //target.GetComponent<PlayerHealth>().TakeDamage(damage);
        target.TakeDamage(damage);
        Debug.Log(name+ "bang bang");
    }
}

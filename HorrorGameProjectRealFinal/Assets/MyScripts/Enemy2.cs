using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    public Player player;
    public float attackDistance;
    public float chaseDistance;
    public int damage;
    public int health;
    public int ScoreToGive;

    private bool isAttacking;
    private bool isDead;

    public NavMeshAgent agent;
    public Animator anim;
    public GameObject bloodEffect;
    public AudioSource audioSource;

    private void Update()
    {
        if (isDead)
            return;

        if(Vector3.Distance(transform.position, player.transform.position)< attackDistance)
        {
            agent.isStopped = true;
            //attack code
         
            if (!isAttacking)
                Attack();
        }
        else
        {
            if(Vector3.Distance(transform.position, player.transform.position)< chaseDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                anim.SetBool("Idle", false);
                anim.SetBool("Running", true);

            }
            else
            {
                agent.isStopped = true;
                anim.SetBool("Idle", true);
                anim.SetBool("Running", false);
            }
      
           

           
        }
    }

    void Attack()
    {
        isAttacking = true;
        anim.SetBool("Running", false);
        anim.SetTrigger("Attack");
        Invoke("TryDamage", 1.2f);
        //Invoke gives us an ability to delay a function in a specified time frame 
        Invoke("DisableIsAttacking", 2.5f);
    }

    void TryDamage()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            player.TakeDamage(damage);
        }
    }

    void DisableIsAttacking()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damageToTake)
    {
        health -= damageToTake;

        if(health<=0)
        {

            anim.SetTrigger("Die");
            isDead = true;
            agent.isStopped = true;
           

            //disable animations
           
            GameManager.instance.AddScore(ScoreToGive);
            //audioSource.clip = null;

           

            GetComponent<Collider>().enabled = false;
            GameObject obj = Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(obj, 5f);
            Destroy(gameObject, 5f);
        }
    }

}

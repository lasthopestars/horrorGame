using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float lifeTime;
    private float shootTime;
    public GameObject HitEffect;


    private void OnEnable()
    {
        shootTime = Time.time;
    }

    private void Update()
    {
        //disable the bullet after lifetime
        if (Time.time - shootTime >= lifeTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
            other.GetComponent<Player>().TakeDamage(damage);
        else if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
         
            GameObject obj = Instantiate(HitEffect, transform.position, Quaternion.identity);
            Destroy(obj, 0.5f);
        }
        else if (other.CompareTag("Enemy2"))
        {
            other.GetComponent<Enemy2>().TakeDamage(damage);
            
            GameObject obj = Instantiate(HitEffect, transform.position, Quaternion.identity);
            Destroy(obj, 0.5f);
        }


        
        gameObject.SetActive(false);
    }

}

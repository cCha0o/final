using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile3DController : MonoBehaviour
{
    //My components
    public Rigidbody RB;
    
    //How fast do I fly?
    public float Speed = 30;
    //How hard do I knockback things I hit?
    public float Knockback = 10;

    void Start()
    {
        //When I spawn, I fly straight forwards at my Speed
        RB.linearVelocity = transform.forward * Speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        // Knockback
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(RB.linearVelocity.normalized * Knockback,ForceMode.Impulse);
        }

        // Damage
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(25);
        }

        Destroy(gameObject);
    }

}

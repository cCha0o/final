using UnityEngine;

public class Projectile3DController : MonoBehaviour
{
    // My components
    public Rigidbody RB;

    // How fast do I fly?
    public float Speed = 30;

    // How hard do I knockback things I hit?
    public float Knockback = 30;

    void Start()
    {
        // When I spawn, I fly straight forwards at my Speed
        RB.linearVelocity = transform.forward * Speed;
    }

    private void OnCollisionEnter(Collision other)
    {   //knockback
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(
                RB.linearVelocity.normalized * Knockback,
                ForceMode.Impulse
            );
        }
          NPCController npc = other.gameObject.GetComponent<NPCController>();
        if (npc != null)
        {
            Vector3 dir = other.transform.position - transform.position;
            npc.ApplyKnockback(dir, Knockback);
        }
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(10);
        }
    }

    Destroy(gameObject);
}
}


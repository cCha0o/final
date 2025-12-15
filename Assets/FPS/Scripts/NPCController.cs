using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Rigidbody RB;
    public Animator Anim;
    public float Speed = 1f;
    public int damage = 10;

    public GameObject Target;

    void Start()
    {
        Anim.Play("Walking");
    }

    private void FixedUpdate()
    {
        if (isKnockedBack || Target == null) return;

        transform.LookAt(Target.transform);

        Vector3 dir = (Target.transform.position - transform.position).normalized;
        dir.y = 0f;

        RB.linearVelocity = dir * Speed;
    }
    public void ApplyKnockback(Vector3 dir, float force)
    {
        if (isKnockedBack) return;

        isKnockedBack = true;

        dir.y = 0f;
        dir.Normalize();

        RB.linearVelocity = Vector3.zero;
        RB.AddForce(dir * force, ForceMode.Impulse);

        Invoke(nameof(RecoverFromKnockback), knockbackRecoveryTime);
    }

    void RecoverFromKnockback()
    {
        isKnockedBack = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        FirstPersonController player =
            collision.gameObject.GetComponent<FirstPersonController>();

        if (player != null)
        {
            player.PlayerTakeDmg(damage);

            Vector3 hitDir =
                collision.transform.position - transform.position;

            player.ApplyHit(hitDir);
        }
    }
    bool isKnockedBack;
    public float knockbackRecoveryTime = 0.3f;

}

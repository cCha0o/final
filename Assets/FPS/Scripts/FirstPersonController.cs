using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] HealthBar _healthbar;

    // The camera is inside the player
    public Camera Eyes;

    public Rigidbody RB;
    public Projectile3DController ProjectilePrefab;

    // Character stats
    public float MouseSensitivity = 3;
    public float CurrentSpeed = 10;
    public float WalkSpeed = 10;
    public float sprintSpeed = 20;
    public float JumpPower = 7;

    // Combat
    public float hitKnockbackForce = 18f;
    public float hitStunTime = 0.2f;
    bool isStunned;

    // A list of all the solid objects I'm currently touching
    public List<GameObject> Floors = new List<GameObject>();

    void Start()
    {
        CurrentSpeed = WalkSpeed;

        // Turn off my mouse and lock it to center screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Subscribe health bar to player health
        GameManager.gameManager._playerHealth.OnHealthChanged += UpdateHealthUI;
    }

    void Update()
    {
        if (isStunned) return;

        // If my mouse goes left/right my body moves left/right
        float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.Rotate(0, xRot, 0);

        // If my mouse goes up/down my aim (but not body) go up/down
        float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
        Eyes.transform.Rotate(yRot, 0, 0);

        // Movement code
        Vector3 move = Vector3.zero;

        // transform.forward/right are relative to the direction my body is facing
        if (Input.GetKey(KeyCode.W)) move += transform.forward;
        if (Input.GetKey(KeyCode.S)) move -= transform.forward;
        if (Input.GetKey(KeyCode.A)) move -= transform.right;
        if (Input.GetKey(KeyCode.D)) move += transform.right;

        move = move.normalized * CurrentSpeed;

        // Jumping
        if (JumpPower > 0 && Input.GetKeyDown(KeyCode.Space) && OnGround())
            move.y = JumpPower;
        else
            move.y = RB.linearVelocity.y;

        RB.linearVelocity = move;

        // Sprinting
        CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : WalkSpeed;

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(
                ProjectilePrefab,
                Eyes.transform.position + Eyes.transform.forward,
                Eyes.transform.rotation
            );
        }
    }

    // I count as being on the ground if I'm touching at least one solid object
    public bool OnGround()
    {
        return Floors.Count > 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!Floors.Contains(other.gameObject))
            Floors.Add(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        Floors.Remove(other.gameObject);
    }

    public void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
         if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            GameStateManager.Instance.LoseGame();
        }
    }

    public void ApplyHit(Vector3 hitDirection)
    {
        if (isStunned) return;

        isStunned = true;

        hitDirection.y = 0f;
        hitDirection.Normalize();
        RB.linearVelocity = Vector3.zero;

        RB.AddForce(hitDirection * hitKnockbackForce, ForceMode.Impulse);
        Invoke(nameof(RecoverFromHit), hitStunTime);
    }


    void RecoverFromHit()
    {
        isStunned = false;
    }

    void UpdateHealthUI(int current, int max)
    {
        _healthbar.SetMaxHealth(max);
        _healthbar.SetHealth(current);
    }

    private void OnDestroy()
    {
        if (GameManager.gameManager != null)
            GameManager.gameManager._playerHealth.OnHealthChanged -= UpdateHealthUI;
    }
    
}

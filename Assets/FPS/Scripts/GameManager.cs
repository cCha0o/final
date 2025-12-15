using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public UnitHealth _playerHealth;

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;

        _playerHealth = new UnitHealth(100, 100);
    }
}
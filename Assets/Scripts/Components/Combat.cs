using UnityEngine;

public class Combat : MonoBehaviour
{
    public float maxHealth = 10;
    public float currentHealth;

    public void Start() {
        currentHealth = maxHealth;
    }

    public void Damage(float amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) { Die(); }
    }

    private void Die() {
        Debug.Log("Died.");
    }
}

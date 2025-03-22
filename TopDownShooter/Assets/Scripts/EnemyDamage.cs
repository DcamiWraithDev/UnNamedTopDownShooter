using UnityEngine;

public class EnemyDamage2D : MonoBehaviour
{
    public int DamageAmount = 25;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthSystem>()?.TakeDamage(DamageAmount);
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    private float damage = 10f;

    private Entity spawner;
    
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Entity entity, Vector2 direction, float speed) {
        spawner = entity;

        Vector3 launchDirection = new Vector3(direction.x, direction.y, 0);
        rb.velocity = launchDirection * speed;
    }

    // TODO
    void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();
        if (entity == spawner) { return;}

        Projectile projectile = collision.GetComponent<Projectile>();
        if (projectile != null) { return;}

        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null) { damageable.Damage(damage); }

        gameObject.SetActive(false);
    }
}

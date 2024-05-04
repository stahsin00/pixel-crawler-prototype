using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public float startAngle = 0f, endAngle = 360f;
    public int bulletsAmount = 10;

    public bool isRandom = false;

    public float speed = 10f;

    private Entity entity;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    public void Fire() {
        if (isRandom) {
            RandomRotation();
        } else { 
            DistributedRotation(); 
        }
    }

    public void ToggleRandom() {
        isRandom = !isRandom;
    }

    private void DistributedRotation()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount; i++)
        {
            SpawnAtAngle(angle);
            angle += angleStep;
        }
    }

    private void RandomRotation()
    {
        for (int i = 0; i < bulletsAmount; i++)
        {
            float angle = Random.Range(startAngle, endAngle);
            SpawnAtAngle(angle);
        }
    }

    private void SpawnAtAngle(float angle) {
        float dirX = transform.position.x + Mathf.Sin(angle * Mathf.PI / 180f);
        float dirY = transform.position.y + Mathf.Cos(angle * Mathf.PI / 180f);

        Vector3 projectileDir = new Vector3(dirX, dirY, 0f);
        Vector2 dir = (projectileDir - transform.position).normalized;

        SpawnProjectile(dir);
    }

    private void SpawnProjectile(Vector2 direction)
    {
        GameObject projectileGO = ObjectPool.Instance.GetProjectile();
        projectileGO.transform.position = transform.position;
        projectileGO.transform.rotation = transform.rotation;
        projectileGO.SetActive(true);

        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.Launch(entity,direction,speed);
    }
}

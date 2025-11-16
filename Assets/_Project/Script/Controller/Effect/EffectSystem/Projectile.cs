using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private float speed;
    private float damage;

    public void SetTarget(GameObject target) { this.target = target; }
    public void SetSpeed(float speed) { this.speed = speed; }
    public void SetDamage(float damage) { this.damage = damage; }

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.deltaTime
        );
    }

    void OnDisable()
    {
        target = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (damage <= 0) return;

        if (other.gameObject == target)
        {
            if (other.TryGetComponent(out EnemyController enemy))
            {
                enemy.TakeDamage(damage);
            }

            PoolService.Despawn(gameObject);
        }
    }
}
using UnityEngine;

public class EnemyData : IDamageable
{
    public EnemyRecord enemyRecord;
    public float currentHp;

    public EnemyData(EnemyRecord record)
    {
        enemyRecord = record;
        currentHp = record.hp;
    }

    public void TakeDamage(float damage)
    {
        currentHp = Mathf.Max(0, currentHp - damage);
    }

    public float DealDamage()
    {
        return enemyRecord.damage;
    }
}
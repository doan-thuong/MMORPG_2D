using UnityEngine;

public static class EnemyService
{
    public static EnemyData enemyData;
    public static EnemyConfig enemyConfig;

    public static void TakeDamage(float damage)
    {
        if (enemyData == null)
        {
            Debug.LogError("Enemy data is not initialized.");
            return;
        }

        enemyData.TakeDamage(damage);
    }

    public static bool IsDead()
    {
        if (enemyData == null)
        {
            Debug.LogError("Enemy data is not initialized.");
            return false;
        }

        return enemyData.currentHp <= 0;
    }

    public static EnemyRecord GetEnemy(string id)
    {
        if (enemyConfig == null)
        {
            Debug.LogError("Enemy config is null");
            return null;
        }

        return enemyConfig.data.Find(r => r.id == id);
    }
}
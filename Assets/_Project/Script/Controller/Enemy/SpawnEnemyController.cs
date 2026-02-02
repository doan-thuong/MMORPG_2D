using System.Collections;
using UnityEngine;

public class SpawnEnemyController : MonoBehaviour
{
    [SerializeField] private EnemySpawnConfig spawnConfig;

    void Start()
    {
        if (spawnConfig.enemySpawns.Count == 0)
        {
            Debug.LogError("List config enemy spawn null");
            return;
        }
        SpawnEnemy();
    }

    void OnEnable()
    {
        EventManager.StartListeningEvent(EventName.Enemy.ENEMY_DIE, OnEnemyDied);
        EventManager.StartListeningEvent(EventName.Enemy.ENEMY_SPAWN, CallSpawnEnemy);
    }

    void OnDisable()
    {
        EventManager.StopListeningEvent(EventName.Enemy.ENEMY_DIE, OnEnemyDied);
        EventManager.StopListeningEvent(EventName.Enemy.ENEMY_SPAWN, CallSpawnEnemy);
    }

    void CallSpawnEnemy()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        foreach (var item in spawnConfig.enemySpawns)
        {
            GameObject gObject = item.prefab;
            Vector3 pos = item.spawnPoint;

            var obInstance = Instantiate(gObject, pos, Quaternion.identity, gameObject.transform);

            if (obInstance != null)
            {
                obInstance.name = gObject.name;
                var enemyCtrl = obInstance.GetComponent<EnemyController>();
                enemyCtrl.OriginalSpawnPoint = pos;
            }
        }
    }

    void OnEnemyDied(object data)
    {
        EnemyController enemy = data as EnemyController;
        if (enemy != null)
        {
            StartCoroutine(RespawnAfterDelay(enemy));
        }
    }

    IEnumerator RespawnAfterDelay(EnemyController enemy)
    {
        enemy.SetDataHpBar(enemy.GetMaxHp());
        yield return new WaitForSeconds(enemy.GetTimeSpawn());

        enemy.transform.position = enemy.OriginalSpawnPoint;
        enemy.gameObject.SetActive(true);
    }
}
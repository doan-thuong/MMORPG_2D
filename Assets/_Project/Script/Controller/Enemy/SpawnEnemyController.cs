using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnEnemyController : MonoBehaviour
{
    [SerializeField] private EnemySpawnConfig spawnConfig;
    private int timeDelaySpawn = 5;

    void Start()
    {
        if (spawnConfig.enemySpawns.Count == 0)
        {
            Debug.LogError("List config enemy spawn null");
            return;
        }
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
                var enemyCtrl = obInstance.GetComponent<EnemyController>();

                enemyCtrl.OriginalSpawnPoint = pos;

                //bỏ đăng ký trước khi đăng ký lại tránh trường hợp event bị đăng ký trùng lặp
                enemyCtrl.OnDie -= RespawnEnemy;
                enemyCtrl.OnDie += RespawnEnemy;
            }
        }
    }

    async void RespawnEnemy(EnemyController enemy)
    {
        enemy.SetDataHpBar(enemy.GetMaxHp());
        await Task.Delay(timeDelaySpawn * 1000);

        enemy.transform.position = enemy.OriginalSpawnPoint;
        enemy.gameObject.SetActive(true);
    }
}
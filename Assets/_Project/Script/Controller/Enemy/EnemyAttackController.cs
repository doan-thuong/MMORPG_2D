using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private RangeController range;
    [SerializeField] private EnemyController enemyController;

    private EnemyRecord enemyData;

    private GameObject currentTarget;
    private bool isProvoked = false;
    private float lastAttackTime = -99f;

    void Start()
    {
        if (range == null)
        {
            Debug.LogError("range null");
            range = GetComponentInChildren<RangeController>();
        }
        enemyController = GetComponent<EnemyController>();
        enemyData = enemyController.GetDataEnemy();
    }

    void Update()
    {
        if (!isProvoked || currentTarget == null) return;

        if (!range.CheckObjectInRange(currentTarget)) return;

        TryAttack();
    }

    void OnEnable()
    {
        EventManager.StartListeningEvent(EventName.Enemy.ENEMY_PROVOCATIVE, HanldeProvocative);
    }

    void OnDisable()
    {
        EventManager.StopListeningEvent(EventName.Enemy.ENEMY_PROVOCATIVE, HanldeProvocative);
        ResetData();
    }

    void HanldeProvocative(object data)
    {
        if (data is DataProvocative provocative && provocative.owner == gameObject)
        {
            isProvoked = true;
            currentTarget = provocative.target;
        }
    }

    private void TryAttack()
    {
        if (Time.time < lastAttackTime + enemyData.attackCooldown) return;

        lastAttackTime = Time.time;
        PerformAttack();
    }

    private void PerformAttack()
    {
        GameObject projectile = SpawnProjectile();

        if (projectile == null)
        {
            Debug.LogError("Projectile spawn failed! Check prefab path.");
            return;
        }

        var projComp = projectile.GetComponent<EnemyProjectile>();
        if (projComp != null)
        {
            projComp.SetTarget(currentTarget);
            projComp.SetSpeed(enemyData.projectileSpeed);
            projComp.SetDamage(enemyData.damage);
        }
        else
        {
            Debug.LogError("Projectile prefab missing Projectile component!");
        }
    }

    private GameObject SpawnProjectile()
    {
        var path = string.Format(PathResource.PATH_PREFAB_SKILL_ITEM, "EnemyProjectile");

        GameObject projectileNew = PoolService.SpawnOther(path, transform.position);
        return projectileNew;
    }

    private void ResetData()
    {
        isProvoked = false;
        currentTarget = null;
        lastAttackTime = -99f;
    }
}
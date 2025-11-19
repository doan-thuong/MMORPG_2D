using System;
using Ilumisoft.HealthSystem;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Health hpBar;
    [SerializeField] private GameObject gObject;
    private EnemyAnimatorService animatorService;
    [SerializeField] private EnemyConfig enemyConfig;
    private EnemyRecord enemyRecord;
    public Action<EnemyController> OnDie;
    [SerializeField] private string id;
    public float hp = 0f;
    public Vector3 OriginalSpawnPoint;

    void Awake()
    {
        gObject.SetActive(true);

        animatorService = GetComponent<EnemyAnimatorService>();
        hpBar = GetComponent<Health>();

        EnemyService.enemyConfig = enemyConfig;
    }

    void OnEnable()
    {
        enemyRecord = EnemyService.GetEnemy(id);
        hp = enemyRecord.hp;
        SetDataHpBar(hp);
    }

    public void SetDataHpBar(float hpValue)
    {
        hpBar.MaxHealth = hpValue;
        hpBar.SetHealth(hpValue);
    }

    public float GetMaxHp()
    {
        return enemyRecord.hp;
    }

    public void TakeDamage(float damage)
    {
        hp = Mathf.Max(0, hp - damage);

        hpBar.ApplyDamage(damage);
        Debug.Log("enemy take damage");

        if (hp <= 0)
        {
            Die();
            EventManager.EmitEvent(EventName.Enemy.ENEMY_DIE, this);
        }
        else
        {
            animatorService.SetAnimGetHit();
        }
    }

    public float DealDamage()
    {
        return enemyRecord.damage;
    }

    public EnemyRecord GetDataEnemy()
    {
        return enemyRecord;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        OnDie?.Invoke(this);
    }
}
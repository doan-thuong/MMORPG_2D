using Ilumisoft.HealthSystem;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Health hpBar;
    [SerializeField] private GameObject gObject;
    private EnemyAnimatorService animatorService;
    [SerializeField] private EnemyConfig enemyConfig;
    private EnemyRecord enemyRecord;
    [SerializeField] private string id;
    public float hp = 0f;

    void Awake()
    {
        gObject.SetActive(true);

        animatorService = GetComponent<EnemyAnimatorService>();
        hpBar = GetComponent<Health>();

        EnemyService.enemyConfig = enemyConfig;
    }

    void Start()
    {
        enemyRecord = EnemyService.GetEnemy(id);
        hp = enemyRecord.hp;
        hpBar.MaxHealth = hp;
        hpBar.SetHealth(hp);
    }

    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        hp = Mathf.Max(0, hp - damage);

        hpBar.ApplyDamage(damage);

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            animatorService.SetAnimGetHit();
        }
    }
}
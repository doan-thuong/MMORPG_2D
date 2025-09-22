using UnityEngine;

[RequireComponent(typeof(HeroController))]
public class AttackController : MonoBehaviour
{
    private RangeController rangeController;
    private HeroController heroController;

    void Start()
    {
        rangeController = GetComponentInChildren<RangeController>();
        heroController = GetComponent<HeroController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject target = rangeController.GetObjectNearest();
            if (target != null)
            {
                EnemyController enemyCtrl = target.GetComponentInParent<EnemyController>();
                if (enemyCtrl == null)
                {
                    Debug.LogError("Target null");
                    return;
                }
                enemyCtrl.TakeDamage(heroController.attackHero);
            }
            else
            {
                Debug.Log("get target nearest null");
            }
        }
    }
}
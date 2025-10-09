using UnityEngine;

[RequireComponent(typeof(HeroController))]
public class AttackController : MonoBehaviour
{
    private RangeController rangeController;
    private HeroController heroController;
    public GameObject currentTarget;

    void Start()
    {
        rangeController = GetComponentInChildren<RangeController>();
        heroController = GetComponent<HeroController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTarget == null || !rangeController.CheckObjectInRange(currentTarget))
            {
                currentTarget = rangeController.GetObjectNearest();
            }

            if (currentTarget != null && rangeController.CheckObjectInRange(currentTarget))
            {
                EnemyController enemyCtrl = currentTarget.GetComponentInParent<EnemyController>();
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
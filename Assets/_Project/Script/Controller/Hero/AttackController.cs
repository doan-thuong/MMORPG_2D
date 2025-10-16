using UnityEngine;

[RequireComponent(typeof(HeroController))]
public class AttackController : MonoBehaviour
{
    private RangeController rangeController;
    private HeroController heroController;
    public GameObject currentTarget;
    [SerializeField] private SkillConfig skillConfig;
    private SkillRecord skillRecord;

    void Awake()
    {
        SkillService.skillConfig = skillConfig;
    }

    void Start()
    {
        rangeController = GetComponentInChildren<RangeController>();
        heroController = GetComponent<HeroController>();
    }

    void OnEnable()
    {
        EventManager.StartListeningEvent(EventName.Skill.USE_SKILL, HandleUseSkill);
    }

    void OnDisable()
    {
        EventManager.StopListeningEvent(EventName.Skill.USE_SKILL, HandleUseSkill);
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
                enemyCtrl.TakeDamage(skillRecord.damage);
                Debug.Log($"damage: {skillRecord.damage}");
            }
            else
            {
                Debug.Log("get target nearest null");
            }
        }
    }

    void HandleUseSkill(object data)
    {
        string idSkill = data.ToString();
        skillRecord = SkillService.GetSkill(idSkill);
    }
}
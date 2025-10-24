using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    private RangeController rangeController;
    [SerializeField] private GameObject btnAttack;
    [SerializeField] private HeroController heroController;
    public GameObject currentTarget;
    [SerializeField] private SkillConfig skillConfig;
    private ISkill currentSkill;

    void Awake()
    {
        SkillService.skillConfig = skillConfig;
    }

    void Start()
    {
        heroController = GetComponent<HeroController>();
        rangeController = GetComponentInChildren<RangeController>();

        var btn = btnAttack.GetComponent<Button>();
        btn.onClick.AddListener(() => HandleAttack());
    }

    void OnEnable()
    {
        EventManager.StartListeningEvent(EventName.Skill.USE_SKILL, HandleUseSkill);
    }

    void OnDisable()
    {
        EventManager.StopListeningEvent(EventName.Skill.USE_SKILL, HandleUseSkill);
    }

    void HandleAttack()
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

            if (currentSkill == null)
            {
                Debug.LogError("Skill is null");
                return;
            }

            EventManager.EmitEvent(EventName.Enemy.ENEMY_NEAREST, currentTarget);

            if (currentSkill.Cast(currentTarget))
                heroController.UpdateMana(currentSkill.CostMana());
        }
        else
        {
            Debug.Log("get target nearest null");
        }
    }

    void HandleUseSkill(object data)
    {
        string idSkill = data.ToString();
        // Debug.Log($"current skill id : {idSkill}");
        currentSkill = SkillService.CreateSkill(idSkill) ?? currentSkill;
    }
}
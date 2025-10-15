using UnityEngine;

public class SkillController : MonoBehaviour
{
    public string id;
    [SerializeField] private SkillConfig skillConfig;
    private SkillRecord skillRecord;

    void Awake()
    {
        SkillService.skillConfig = skillConfig;
    }

    void Start()
    {
        skillRecord = SkillService.GetSkill(id);
    }

    public SkillRecord GetDataSkill()
    {
        return skillRecord;
    }
}
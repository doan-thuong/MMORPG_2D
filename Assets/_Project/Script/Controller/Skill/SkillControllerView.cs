using UnityEngine;
using UnityEngine.UI;

public class SkillControllerView : MonoBehaviour
{
    [SerializeField] private GameObject usingObj;
    [SerializeField] private GameObject activeObj;
    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickSkill);
    }

    void OnClickSkill()
    {
        EventManager.EmitEvent(EventName.Skill.CHOOSE_SKILL, gameObject);
    }

    public bool ActiveSkill()
    {
        if (!activeObj.activeSelf)
        {
            usingObj.SetActive(true);
            return true;
        }

        return false;
    }

    public void DeactiveSkill()
    {
        usingObj.SetActive(false);
    }

    void OnDestroy()
    {
        btn.onClick.RemoveListener(OnClickSkill);
    }
}
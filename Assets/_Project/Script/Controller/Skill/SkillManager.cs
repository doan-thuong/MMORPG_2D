using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Biến **parents** sau này sẽ lấy từ data player 
    [SerializeField] private List<GameObject> parents;
    [SerializeField] private GameObject defaultSkill;
    private Dictionary<GameObject, SkillControllerView> skillViews;

    void Awake()
    {
        InitSkillView();
    }

    void Start()
    {
        if (defaultSkill != null)
        {
            SetActiveByName(defaultSkill);
            EventManager.EmitEvent(EventName.Skill.USE_SKILL, defaultSkill.name);
        }
        else
            Debug.LogError("Skill default null");
    }

    void InitSkillView()
    {
        skillViews = new();
        foreach (var obj in parents)
        {
            if (obj == null)
            {
                Debug.LogError("Null object when init dict skillViews");
                return;
            }

            var skillView = obj.GetComponent<SkillControllerView>();

            if (skillView == null)
            {
                Debug.LogError("Get component SkillControllerView null");
            }
            skillViews.Add(obj, skillView);
        }
    }

    void SetActiveByName(GameObject gameObj)
    {
        if (skillViews.ContainsKey(gameObj))
        {
            if (skillViews[gameObj].ActiveSkill())
            {
                foreach (var item in skillViews)
                {
                    if (item.Key == gameObj) continue;
                    else item.Value.DeactiveSkill();
                }
            }
        }
    }

    void OnEnable()
    {
        EventManager.StartListeningEvent(EventName.Skill.CHOOSE_SKILL, HandleChooseSkill);
    }

    void OnDisable()
    {
        EventManager.StopListeningEvent(EventName.Skill.CHOOSE_SKILL, HandleChooseSkill);
    }

    private void HandleChooseSkill(object gameObj)
    {
        var objParse = gameObj as GameObject;
        SetActiveByName(objParse);

        var nameSkill = objParse.name;
        EventManager.EmitEvent(EventName.Skill.USE_SKILL, nameSkill);
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    // Biến **parents** sau này sẽ lấy từ data player 
    [SerializeField] private List<GameObject> parents;

    void Start()
    {

        foreach (var obj in parents)
        {
            Button btn = obj.GetComponent<Button>();

            if (btn == null)
            {
                Debug.LogError($"Not found button in {btn.name}");
            }

            btn.onClick.AddListener(() => OnAnyClickButton(obj, parents));
        }
    }

    private void OnAnyClickButton(GameObject target, List<GameObject> parents)
    {
        SkillBarService.OnClickSetUsingObject(target, parents);
        var nameSkill = target.name;
        EventManager.EmitEvent(EventName.Skill.USE_SKILL, nameSkill);
    }
}
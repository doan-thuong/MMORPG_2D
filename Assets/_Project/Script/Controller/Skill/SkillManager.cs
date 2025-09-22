using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> parents;
    private SkillBarService skillService;

    void Start()
    {
        skillService = new();

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
        skillService.OnClickSetUsingObject(target, parents);
    }
}
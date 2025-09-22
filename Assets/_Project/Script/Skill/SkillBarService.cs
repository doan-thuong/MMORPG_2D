using System.Collections.Generic;
using UnityEngine;

public class SkillBarService
{
    public void OnClickSetUsingObject(GameObject target, List<GameObject> parents)
    {
        Transform objUsing = target.transform.Find("using");
        Transform objActive = target.transform.Find("inactive");

        if (!objUsing || !objActive)
        {
            Debug.LogError("Not find object using or active");
            return;
        }

        if (objActive.gameObject.activeSelf) return;

        OnClickSetUnusingObject(parents);

        objUsing.gameObject.SetActive(true);
    }

    public void OnClickSetUnusingObject(List<GameObject> parents)
    {
        Transform objUsing;
        foreach (var p in parents)
        {
            objUsing = p.transform.Find("using");

            objUsing.gameObject.SetActive(false);
        }
    }
}
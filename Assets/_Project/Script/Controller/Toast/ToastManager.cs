using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ToastManager : MonoBehaviour
{
    // [SerializeField] private TextMeshPro textMeshPro;
    // [SerializeField] private GameObject toastObject;

    void OnEnable()
    {
        EventManager.StartListeningEvent(EventName.Toast.PUSH_TOAST, HandlePushToastNoti);
    }

    void OnDestroy()
    {
        EventManager.StopListeningEvent(EventName.Toast.PUSH_TOAST, HandlePushToastNoti);
    }

    private void HandlePushToastNoti(object data)
    {
        Debug.Log("nghe");
        if (data is ToastStruct toastData)
        {
            string path = PathResource.PATH_PREFAB_TOAST_NOTI;
            GameObject toastObject = PoolService.SpawnOther(path, new Vector3(960, -675, 0), null, gameObject.transform);

            var textMeshPro = toastObject.GetComponent<ToastView>();
            textMeshPro.SetTextMeshPro(toastData.mess);

            toastObject.transform.DOMoveY(540, 1f);

            StartCoroutine(AutoDespawn(toastObject, toastData.toastLifeTime));
        }
        else
        {
            Debug.LogError("Type of data is not ToastStruct");
            return;
        }
    }

    private IEnumerator AutoDespawn(GameObject toastGO, float toastLifeTime)
    {
        yield return new WaitForSeconds(toastLifeTime);
        PoolService.Despawn(toastGO, new Vector3(960, -675, 0));
    }
}
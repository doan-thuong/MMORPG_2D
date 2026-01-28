using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ToastManager : SingletonBehaviour<ToastManager>
{
    [SerializeField] private Vector3 positionDefault = new(960, -675, 0);
    [SerializeField] private float posY = 540f;

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
        if (data is ToastStruct toastData)
        {
            string path = PathResource.PATH_PREFAB_TOAST_NOTI;
            GameObject toastObject = PoolService.SpawnOther(path, positionDefault, null, gameObject.transform);

            var textMeshPro = toastObject.GetComponent<ToastView>();
            textMeshPro.SetTextMeshPro(toastData.mess);

            toastObject.transform.DOMoveY(posY, 1f);

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
        PoolService.Despawn(toastGO, positionDefault);
    }
}
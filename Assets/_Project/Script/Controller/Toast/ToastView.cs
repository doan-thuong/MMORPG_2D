using TMPro;
using UnityEngine;

public class ToastView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public void SetTextMeshPro(string mes)
    {
        textMeshPro.text = mes;
    }
}
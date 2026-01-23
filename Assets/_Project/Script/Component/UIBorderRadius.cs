using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class UIBorderRadius : MonoBehaviour
{
    [Header("Border Radius Settings")]
    [SerializeField][Range(0f, 100f)] private float topLeftRadius = 0f;
    [SerializeField][Range(0f, 100f)] private float topRightRadius = 0f;
    [SerializeField][Range(0f, 100f)] private float bottomLeftRadius = 0f;
    [SerializeField][Range(0f, 100f)] private float bottomRightRadius = 0f;

    [SerializeField] private Color borderColor = Color.white;

    private Image image;
    private Material material;

    void Start()
    {
        image = GetComponent<Image>();
        SetupMaterial();
    }

    void OnValidate()
    {
        if (image != null)
        {
            UpdateRadius();
        }
    }

    private void SetupMaterial()
    {
        // Tạo material mới từ shader mặc định của Unity UI, nhưng chúng ta cần shader custom cho border radius
        // Lưu ý: Bạn cần tạo một Shader custom cho border radius (ví dụ dùng RoundedRect shader từ Asset Store hoặc tự viết)
        // Ở đây giả sử bạn có shader "UI/RoundedCorners"
        material = new Material(Shader.Find("UI/RoundedCorners"));
        image.material = material;
        UpdateRadius();
    }

    private void UpdateRadius()
    {
        if (material != null)
        {
            material.SetFloat("_TopLeftRadius", topLeftRadius);
            material.SetFloat("_TopRightRadius", topRightRadius);
            material.SetFloat("_BottomLeftRadius", bottomLeftRadius);
            material.SetFloat("_BottomRightRadius", bottomRightRadius);
            material.SetColor("_BorderColor", borderColor);
        }
    }
}
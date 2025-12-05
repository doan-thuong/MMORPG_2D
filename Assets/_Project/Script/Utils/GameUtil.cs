using UnityEngine;

public static class GameUtil
{
    /// <summary>
    /// Kiểm tra xem object có thuộc layer mask đã cho không.
    /// </summary>
    public static bool IsInLayer(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) != 0;
    }
}
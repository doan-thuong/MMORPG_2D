using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GateRecord gateRecord;
    [SerializeField] private string mapId;
    [SerializeField] private string nextGateId;
    [SerializeField] private GateConfig gateConfig;

    void Awake()
    {
        GateService.gateConfig = gateConfig;
    }
    void Start()
    {
        mapId = MapInit.Instance.MapId;
        gateRecord = GateService.GetGateRecord(mapId, nextGateId);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (mapId == null)
        {
            Debug.LogError("Map id null");
            return;
        }
        if (GameUtil.IsInLayer(collision.gameObject, layerMask))
        {
            MapInit.Instance.LoadNewMap(gateRecord.nextGateId, gateRecord.playerSpawnPoint);
        }
    }
}
using UnityEngine;

public static class GateService
{
    public static GateConfig gateConfig;
    public static GateRecord gateRecord;

    public static GateRecord GetGateRecord(string idGate, string idNextMap)
    {
        if (gateConfig == null)
        {
            Debug.LogError("Gate data null");
            return null;
        }
        return gateConfig.data.Find(g => g.id == idGate && g.nextGateId == idNextMap);
    }
}
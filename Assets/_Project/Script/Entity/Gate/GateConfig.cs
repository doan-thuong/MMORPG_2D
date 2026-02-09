using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GateRecord
{
    public string id;
    public string nextGateId;
    public Vector3 playerSpawnPoint;
}

[CreateAssetMenu(fileName = "gate_config", menuName = "Custom/gate_config")]
public class GateConfig : ScriptableObject
{
    public List<GateRecord> data = new();
}
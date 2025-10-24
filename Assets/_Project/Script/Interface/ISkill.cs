using UnityEngine;

public interface ISkill
{
    string Id { get; }
    float Cooldown { get; }
    void Initialize(GameObject owner);
    bool CanCast();
    bool Cast(GameObject owner);
    float CostMana();
}
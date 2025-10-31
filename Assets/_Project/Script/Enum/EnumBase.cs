public partial class EnumBase
{
    public enum EffectType
    {
        Damage = 1,
        Projectile = 2,
        Stun = 4,
        Freeze = 5,
    }

    public enum EffectParam
    {
        damage = 1,
        projectileDamage = 2,
        projectileSpeed = 3,
    }
}
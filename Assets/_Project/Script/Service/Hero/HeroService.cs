using UnityEngine;

public static class HeroService
{
    public static HeroData heroData;
    public static HeroConfig heroConfig;

    public static void TakeDamage(float damage)
    {
        if (heroData == null)
        {
            Debug.LogError("Hero data is not initialized.");
            return;
        }

        heroData.TakeDamage(damage);
    }

    public static bool IsDead()
    {
        if (heroData == null)
        {
            Debug.LogError("Hero data is not initialized.");
            return false;
        }

        return heroData.currentHp <= 0;
    }

    public static HeroRecord GetHero(string id)
    {
        if (heroConfig == null)
        {
            Debug.LogError("Hero config is null");
            return null;
        }

        return heroConfig.data.Find(r => r.id == id);
    }
}
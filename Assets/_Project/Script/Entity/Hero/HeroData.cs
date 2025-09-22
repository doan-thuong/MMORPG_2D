using UnityEngine;

public class HeroData
{
    public HeroRecord heroRecord;
    public float currentHp;

    public HeroData(HeroRecord hero)
    {
        heroRecord = hero;
        currentHp = hero.hp;
    }

    public void TakeDamage(float damage)
    {
        currentHp = Mathf.Max(0, currentHp - damage);
    }

    public float DealDamage()
    {
        return heroRecord.damage;
    }
}
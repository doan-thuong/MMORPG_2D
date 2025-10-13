using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    public HpBarService hpBarService;
    public ManaBarService manaBarService;
    public string id;
    [SerializeField] private HeroConfig heroConfig;
    private HeroRecord heroRecord;
    public Slider hpBar;
    public Slider manaBar;
    private float currentHp;
    private float currentMana;
    public float attackHero;

    void Awake()
    {
        HeroService.heroConfig = heroConfig;
    }

    void Start()
    {
        heroRecord = HeroService.GetHero(id);
        if (hpBarService.slider == null)
        {
            hpBarService.slider = hpBar;
        }

        if (manaBarService.slider == null)
        {
            manaBarService.slider = manaBar;
        }

        attackHero = heroRecord.damage;
        InitHPBar();
        InitManaBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UpdateHP(11.5f);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            UpdateMana(10f);
        }
    }

    void InitHPBar()
    {
        hpBarService.SetMaxHealth(heroRecord.hp);
        currentHp = heroRecord.hp;
    }

    void UpdateHP(float hpCost)
    {
        currentHp -= hpCost;
        hpBarService.SetHealth(currentHp);
    }

    void InitManaBar()
    {
        manaBarService.SetMaxMana(heroRecord.mana);
        currentMana = heroRecord.mana;
    }

    void UpdateMana(float manaCost)
    {
        currentMana -= manaCost;
        manaBarService.SetMana(currentMana);
    }
}
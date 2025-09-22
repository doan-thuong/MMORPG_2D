using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    public HpBarService hpBarService;
    public string id;
    [SerializeField] private HeroConfig heroConfig;
    private HeroRecord heroRecord;
    public Slider hpBar;
    private float currentHp;
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

        hpBarService.SetMaxHealth(heroRecord.hp);
        currentHp = heroRecord.hp;
        attackHero = heroRecord.damage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentHp -= 11.5f;
            hpBarService.SetHealth(currentHp);
        }
    }
}
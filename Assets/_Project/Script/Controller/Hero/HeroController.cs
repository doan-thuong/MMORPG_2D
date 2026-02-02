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
    private float maxHp;
    private float maxMana;

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

        maxHp = heroRecord.hp;
        maxMana = heroRecord.mana;

        InitHPBar();
        InitManaBar();
    }

    void OnEnable()
    {
        EventManager.StartListeningEvent(EventName.Hero.SET_POSITION, SetPosition);
    }

    void OnDisable()
    {
        EventManager.StopListeningEvent(EventName.Hero.SET_POSITION, SetPosition);
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

    public void UpdateHP(float hpCost)
    {
        currentHp -= hpCost;

        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        hpBarService.SetHealth(currentHp);
    }

    void InitManaBar()
    {
        manaBarService.SetMaxMana(heroRecord.mana);
        currentMana = heroRecord.mana;
    }

    public void UpdateMana(float manaCost)
    {
        currentMana -= manaCost;

        currentMana = Mathf.Clamp(currentMana, 0, maxMana);

        manaBarService.SetMana(currentMana);
    }

    public float GetCurrentMana()
    {
        return currentMana;
    }

    private void SetPosition(object data)
    {
        gameObject.transform.position = (Vector3)data;

        EventManager.EmitEvent(EventName.Camera.SETUP);
    }
}
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RegenStatController : MonoBehaviour
{
    public float HpRegen;
    public float ManaRegen;
    public float timeDelay;
    private HeroController heroController;
    private bool running;

    void Start()
    {
        heroController = GetComponent<HeroController>();
    }

    async void OnEnable()
    {
        running = true;
        while (running)
        {
            await UniTask.Delay((int)timeDelay * 1000);
            // Debug.Log("regen stat");
            heroController.UpdateHP(-HpRegen);
            heroController.UpdateMana(-ManaRegen);
        }
    }

    void OnDisable()
    {
        running = false;
    }
}
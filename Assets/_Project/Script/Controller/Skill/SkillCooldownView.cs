using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownView : MonoBehaviour
{
    [SerializeField] private GameObject bgCooldown;
    private Image background;
    private Coroutine cooldownCoroutine;
    private bool isCooldownRunning;

    void Start()
    {
        background = bgCooldown.GetComponent<Image>();
    }

    void OnEnable()
    {
        EventManager.StartListeningEvent(EventName.Skill.START_COOLDOWN, HandleCooldown);
    }

    void OnDisable()
    {
        EventManager.StopListeningEvent(EventName.Skill.START_COOLDOWN, HandleCooldown);
    }

    private void HandleCooldown(object data)
    {
        if (data is SkillEventData eventData && eventData.skillId == gameObject.name)
        {
            StartCooldown(eventData.timeCooldown);
        }
    }

    public void ActiveCooldownView()
    {
        if (bgCooldown != null)
        {
            bgCooldown.SetActive(true);
        }
    }

    public void DeactiveCooldownView()
    {
        if (bgCooldown != null)
        {
            bgCooldown.SetActive(false);
        }
    }

    public void StartCooldown(float timeCooldown)
    {
        if (background == null)
        {
            Debug.LogWarning($"background Image không được gán cho {name}.", this);
            return;
        }

        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }

        isCooldownRunning = true;
        cooldownCoroutine = StartCoroutine(CooldownCoroutine(timeCooldown));
    }

    public void ResetCooldown()
    {
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
            cooldownCoroutine = null;
        }

        isCooldownRunning = false;
        if (background != null)
        {
            background.fillAmount = 1f;
        }
        DeactiveCooldownView();
    }

    public bool IsCooldownReady()
    {
        return !isCooldownRunning;
    }

    private IEnumerator CooldownCoroutine(float timeCooldown)
    {
        ActiveCooldownView();
        float elapsedTime = 0f;

        while (elapsedTime < timeCooldown)
        {
            if (background != null)
            {
                background.fillAmount = 1f - (elapsedTime / timeCooldown);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ResetCooldown();
    }

    void OnDestroy()
    {
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
    }
}
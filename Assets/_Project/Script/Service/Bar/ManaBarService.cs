using UnityEngine;
using UnityEngine.UI;

public class ManaBarService : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
    }
}
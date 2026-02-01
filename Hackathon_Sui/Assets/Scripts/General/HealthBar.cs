using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider mySlider;
    public BasicStatus myStatus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySlider.maxValue = myStatus.baseHealthPoint;
        mySlider.value = mySlider.maxValue;
        myStatus.onHealthChange += OnHealthChange;
        myStatus.onHealthChange?.Invoke();
    }

    void OnHealthChange()
    {
        mySlider.value = myStatus.currentHealth;
        if(myStatus.currentHealth <= 0)
        {
            GameObject fillArea = mySlider.transform.Find("Fill Area").gameObject;
            fillArea.SetActive(false);
        }
    }
}

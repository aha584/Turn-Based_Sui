using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChooseStatManager : MonoBehaviour
{
    public static ChooseStatManager Instance;

    public TMP_InputField statPointField;
    public TMP_InputField nameField;
    public Slider hpSlider;
    public Slider atkSlider;
    public Slider defSlider;
    public Slider spdSlider;
    public int currentStatPoint;

    private int statPoint = 175;
    public PlayerStatus playerPet;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStatPoint = statPoint;
        statPointField.text = $"{currentStatPoint}";
        statPointField.ForceLabelUpdate();
        playerPet = new PlayerStatus("Name", 0, 0, 0, 0, 1);
        hpSlider.maxValue = 100;
        hpSlider.value = 0;
        atkSlider.maxValue = 100;
        atkSlider.value = 0;
        defSlider.maxValue = 100;
        defSlider.value = 0;
        spdSlider.maxValue = 100;
        spdSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        statPointField.text = $"{currentStatPoint}";
        statPointField.ForceLabelUpdate();
    }
    public void IncreaseHP()
    {
        if (currentStatPoint <= 0) return;
        if (playerPet == null) return;
        if (hpSlider.value == hpSlider.maxValue) return;
        playerPet.baseHealthPoint++;
        hpSlider.value++;
        currentStatPoint--;
    }
    public void IncreaseATK()
    {
        if (currentStatPoint <= 0) return;
        if (playerPet == null) return;
        if (atkSlider.value == atkSlider.maxValue) return;
        playerPet.baseAttack++;
        atkSlider.value++;
        currentStatPoint--;
    }
    public void IncreaseDEF()
    {
        if (currentStatPoint <= 0) return;
        if (playerPet == null) return;
        if (defSlider.value == defSlider.maxValue) return;
        playerPet.baseDefend++;
        defSlider.value++;
        currentStatPoint--;
    }
    public void IncreaseSPD()
    {
        if (currentStatPoint <= 0) return;
        if (playerPet == null) return;
        if (spdSlider.value == spdSlider.maxValue) return;
        playerPet.baseSpeed++;
        spdSlider.value++;
        currentStatPoint--;
    }
    public void ResetStat()
    {
        playerPet = new PlayerStatus("Name", 0, 0, 0, 0, 1);
        nameField.text = "Name";
        nameField.ForceLabelUpdate();
        hpSlider.value = 0;
        atkSlider.value = 0;
        defSlider.value = 0;
        spdSlider.value = 0;
        currentStatPoint = statPoint;
    }
    public void RandomStat()
    {
        currentStatPoint = statPoint;
        int midStat = (int)(statPoint / 5);
        playerPet.baseHealthPoint = Random.Range(0, midStat);
        hpSlider.value = playerPet.baseHealthPoint;
        currentStatPoint -= (int)(playerPet.baseHealthPoint);
        playerPet.baseAttack = Random.Range(0, midStat);
        atkSlider.value = playerPet.baseAttack;
        currentStatPoint -= (int)(playerPet.baseAttack);
        playerPet.baseDefend = Random.Range(0, midStat);
        defSlider.value = playerPet.baseDefend;
        currentStatPoint -= (int)(playerPet.baseDefend);
        playerPet.baseSpeed = Random.Range(0, midStat);
        spdSlider.value = playerPet.baseSpeed;
        currentStatPoint -= (int)(playerPet.baseSpeed);
    }
    public bool CheckValid()
    {
        if (playerPet.avatar == null || playerPet.baseHealthPoint <= 0 || playerPet.baseSpeed <= 0)
        {
            return false;
        }
        return true;
    }
}

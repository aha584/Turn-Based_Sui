using UnityEngine;

public class HPIncreaseButton : MonoBehaviour
{
    public void OnClick()
    {
        ChooseStatManager.Instance.IncreaseHP();
        SuiManager.Instance.IncreaseSuiCreate();
    }
}

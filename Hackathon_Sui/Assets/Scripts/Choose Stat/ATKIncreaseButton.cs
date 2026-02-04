using UnityEngine;

public class ATKIncreaseButton : MonoBehaviour
{
    public void OnClick()
    {
        ChooseStatManager.Instance.IncreaseATK();
        SuiManager.Instance.IncreaseSuiCreate();
    }
}

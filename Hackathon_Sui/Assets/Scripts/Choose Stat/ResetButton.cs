using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public void OnClick()
    {
        ChooseStatManager.Instance.ResetStat();
        SuiManager.Instance.ResetSuiAmount();
    }
}

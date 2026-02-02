using UnityEngine;

public class SPDIncreaseButton : MonoBehaviour
{
    public void OnClick()
    {
        ChooseStatManager.Instance.IncreaseSPD();
    }
}

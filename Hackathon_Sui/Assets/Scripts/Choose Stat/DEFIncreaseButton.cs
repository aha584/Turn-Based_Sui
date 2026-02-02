using UnityEngine;

public class DEFIncreaseButton : MonoBehaviour
{
    public void OnClick()
    {
        ChooseStatManager.Instance.IncreaseDEF();
    }
}

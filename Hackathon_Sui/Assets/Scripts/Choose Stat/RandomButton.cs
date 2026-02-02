using UnityEngine;

public class RandomButton : MonoBehaviour
{
    public void OnClick()
    {
        ChooseStatManager.Instance.RandomStat();
    }
}

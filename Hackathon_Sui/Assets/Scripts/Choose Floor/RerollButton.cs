using UnityEngine;

public class RerollButton : MonoBehaviour
{
    public void OnClick()
    {
        GenerateEnemy.Instance.Generate();
    }
}

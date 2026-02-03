using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    public void OnClick(int floorIndex)
    {
        VersusInfo.Instance.enemy = GenerateEnemy.Instance.randomEnemy[floorIndex];
        SceneManager.LoadScene("Battle Scene");
    }
}

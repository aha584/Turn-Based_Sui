using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    public async void OnClick(int floorIndex)
    {
        VersusInfo.Instance.enemy = GenerateEnemy.Instance.randomEnemy[floorIndex];
        await SuiManager.Instance.TransferSui(SuiManager.Instance.testAcc, SuiManager.Instance.ownerAcc, (decimal)(SuiManager.Instance.TakeSui(2)));
        SceneManager.LoadScene("Battle Scene");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    public async void OnClick(int floorIndex)
    {
        VersusInfo.Instance.enemy = GenerateEnemy.Instance.randomEnemy[floorIndex];
        await SuiManager.Instance.TransferSuiToOwner(SuiManager.Instance.testAcc, (decimal)(SuiManager.Instance.TakeSui(false)));
        SceneManager.LoadScene("Battle Scene");
    }
}

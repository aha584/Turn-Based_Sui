using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmButton : MonoBehaviour
{
    public async void OnClick()
    {
        if (!ChooseStatManager.Instance.CheckValid())
        {
            Debug.Log("Not Valid!");
            return;
        }
        await SuiManager.Instance.MintNewPet(SuiManager.Instance.testAcc,
                                             ChooseStatManager.Instance.playerPet.myName,
                                             (ulong)ChooseStatManager.Instance.playerPet.baseHealthPoint,
                                             (ulong)ChooseStatManager.Instance.playerPet.baseAttack,
                                             (ulong)ChooseStatManager.Instance.playerPet.baseDefend,
                                             (ulong)ChooseStatManager.Instance.playerPet.baseSpeed,
                                             SuiManager.Instance.suiToCreate);
        ChooseStatManager.Instance.playerPet.currentHealth = ChooseStatManager.Instance.playerPet.baseHealthPoint;
        PlayerPetInfoTransfer.Instance.playerInfo = ChooseStatManager.Instance.playerPet;
        SceneManager.LoadScene("Choose Floor");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmButton : MonoBehaviour
{
    public void OnClick()
    {
        if (!ChooseStatManager.Instance.CheckValid()) return;
        ChooseStatManager.Instance.playerPet.currentHealth = ChooseStatManager.Instance.playerPet.baseHealthPoint;
        PlayerPetInfoTransfer.Instance.playerInfo = ChooseStatManager.Instance.playerPet;
        SceneManager.LoadScene("Choose Floor");
    }
}

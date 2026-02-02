using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmButton : MonoBehaviour
{
    public void OnClick()
    {
        if (!ChooseStatManager.Instance.CheckValid()) return;
        PlayerPetInfoTransfer.Instance.playerInfo = ChooseStatManager.Instance.playerPet;
        SceneManager.LoadScene("Choose Floor");
    }
}

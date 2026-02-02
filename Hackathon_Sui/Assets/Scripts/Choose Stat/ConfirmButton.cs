using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmButton : MonoBehaviour
{
    public void OnClick()
    {
        PlayerPetInfoTransfer.Instance.playerInfo = ChooseStatManager.Instance.playerPet;
        SceneManager.LoadScene("Choose Floor");
    }
}

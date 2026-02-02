using UnityEngine;

public class PlayerPetInfoTransfer : MonoBehaviour
{
    public static PlayerPetInfoTransfer Instance;

    public PlayerStatus playerInfo;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}

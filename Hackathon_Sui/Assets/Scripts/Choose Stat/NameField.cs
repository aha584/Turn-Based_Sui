using UnityEngine;
using TMPro;

public class NameField : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_Text nameAndLevelText;


    public void OnValueChange()
    {
        nameAndLevelText.text = $"{nameField.text} Lv.1";
        nameAndLevelText.ForceMeshUpdate();
    }
    public void OnEndEdit()
    {
        ChooseStatManager.Instance.playerPet.myName = nameField.text;
    }
}

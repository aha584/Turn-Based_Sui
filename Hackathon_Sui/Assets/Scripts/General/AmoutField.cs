using UnityEngine;
using TMPro;

public class AmoutField : MonoBehaviour
{
    public TMP_InputField amountField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        await SuiManager.Instance.GetBalance(SuiManager.Instance.address);
        amountField.text = $"{SuiManager.Instance.suiCoins}";
        amountField.ForceLabelUpdate();
    }
}

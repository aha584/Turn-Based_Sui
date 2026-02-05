using UnityEngine;
using TMPro;

public class AmoutField : MonoBehaviour
{
    public TMP_InputField amountField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        //await SuiManager.Instance.GetBalance(SuiManager.Instance.address);
        Debug.Log("Hex: " + SuiManager.Instance.testAcc.SuiAddress().ToHex());
        Debug.Log("Base 64: " + SuiManager.Instance.testAcc.SuiAddress().ToBase64());
        Debug.Log("String: " + SuiManager.Instance.testAcc.SuiAddress().ToString());
        SuiManager.Instance.playerAddress = SuiManager.Instance.testAcc.SuiAddress().ToHex();
        await SuiManager.Instance.GetBalance(SuiManager.Instance.testAcc.SuiAddress().ToHex());
        amountField.text = $"{SuiManager.Instance.suiCoins}";
        amountField.ForceLabelUpdate();
    }
}

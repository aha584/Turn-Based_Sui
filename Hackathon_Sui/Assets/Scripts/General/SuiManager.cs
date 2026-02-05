using OpenDive.BCS;
using Sui.Accounts;
using Sui.Rpc;
using Sui.Rpc.Client;
using Sui.Rpc.Models;
using Sui.Transactions;
using Sui.Types;
using Sui.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SuiManager : MonoBehaviour
{
    public static SuiManager Instance;

    public SuiClient client;
    public string ownerAddress;
    public string ownerHex;
    public string playerAddress;
    public double suiCoins;
    public Account testAcc;
    public Account ownerAcc;
    [SerializeField] private string mainNet = "https://fullnode.mainnet.sui.io:443";
    [SerializeField] private string devNet = "https://fullnode.devnet.sui.io:443";
    [SerializeField] private string testNet = "https://fullnode.testnet.sui.io:443";
    private const string PACKAGE_ID = "0x1482264abeeecd7f8dfbb047867ffa7642e6fb6ff0e740c231133e26f9ac2c45";
    private const string MODULE_NAME = "pet_module";
    private const string FUNCTION_NAME = "create_pet";

    public double suiToCreate = 0;
    [SerializeField]private double suiToReset = 0;
    [SerializeField]private double suiToReroll = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetUpSlient();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetUpSlient()
    {
        //string pk = "suiprivkey1qrwd7z3kqvp55fshk092t4j59ztz45az5exut3dp3r2vgz0ca9632tlmrml";
        string hex = "dcdf0a3603034a2617b3caa5d65428962ad3a2a64dc5c5a188d4c409f8e97515";

        //RPC Mainnet: "https://fullnode.mainnet.sui.io:443"
        //Testnet: https://fullnode.testnet.sui.io:443
        //Devnet: https://fullnode.devnet.sui.io:443
        var connection = new Connection(testNet);
        client = new SuiClient(connection);

        byte[] keyBytesOwner = Utils.HexStringToByteArray(ownerHex);
        ownerAcc = new(keyBytesOwner);

        byte[] keyBytesPlayer = Utils.HexStringToByteArray(hex);
        Account Acc = new Account(keyBytesPlayer);
        testAcc = Acc;
    }

    public async Task GetBalance(string address)
    {
        string coinType = "0x2::sui::SUI";

        AccountAddress testAddress = new AccountAddress(address);
        SuiStructTag coin_Type = new(coinType);

        var result = await client.GetBalanceAsync(testAddress, coin_Type);

        // 4. Xử lý kết quả trả về
        if (result.Error == null)
        {
            double mist = (double)(result.Result.TotalBalance);
            suiCoins = mist / Mathf.Pow(10, 9);

            //Debug.Log($"Balance của ví {address}: {suiCoins} SUI");
        }
        else
        {
            Debug.LogError($"Lỗi khi lấy balance: {result.Error.Message}");
        }
    }
    public async Task MintNewPet(Account playerAccount, string name, ulong hp, ulong str, ulong def, ulong agi, double suiAmount)
    {
        string safeName = string.IsNullOrEmpty(name) ? "UnknownPet" : name;
        ulong mistAmount = (ulong)((suiAmount + suiToReset + suiToReroll) * 1_000_000_000);

        try
        {
            var tx = new TransactionBlock();

            var nameArg = tx.AddPure(new BString(name));
            TransactionArgument[] args =
            {
                tx.gas,
                tx.AddPure(new U64(mistAmount)), 
                tx.AddPure(new BString(safeName)),
                tx.AddPure(new U64(hp)),
                tx.AddPure(new U64(str)),
                tx.AddPure(new U64(def)),
                tx.AddPure(new U64(agi)),
            };

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null) throw new System.Exception($"Tham số thứ {i} trong mảng args bị NULL!");
            }

            SuiMoveNormalizedStructType target = new SuiMoveNormalizedStructType(PACKAGE_ID, MODULE_NAME, FUNCTION_NAME, new List<SuiMoveNormalizedType>());
            var typeArgs = new List<SerializableTypeTag>();

            tx.AddMoveCallTx(target, typeArgs.ToArray(), args, null);

            var response = await client.SignAndExecuteTransactionBlockAsync(tx, playerAccount);

            if (response == null)
            {
                Debug.LogError("Node không phản hồi (Response null)");
                return;
            }

            if (response.Result != null)
            {
                Debug.Log($"<color=cyan>Thành công!</color> Digest: {response.Result.Digest}");
            }
            else if (response.Error != null)
            {
                Debug.LogError($"Lỗi từ Node: {response.Error.Message}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }
    public async Task TransferSui(Account sender, Account receiver, decimal amountInSui)
    {
        if (sender == null) { Debug.LogError("Sender Account is null!"); return; }
        if (receiver == null) { Debug.LogError("Receiver Account is null!"); return; }
        if (string.IsNullOrEmpty(ownerAddress))
        {
            Debug.LogError("Owner address is empty! Hãy gán địa chỉ ví của bạn vào biến 'address' trong Inspector.");
            return;
        }
        if (string.IsNullOrEmpty(playerAddress))
        {
            Debug.LogError("Player address is empty! Hãy gán địa chỉ ví của bạn vào biến 'address' trong Inspector.");
            return;
        }
        if (amountInSui <= 0)
        {
            Debug.LogWarning("<color=orange>Hủy giao dịch:</color> Số tiền chuyển phải lớn hơn 0 SUI.");
            return;
        }
        ulong amountMist = (ulong)(amountInSui * 1_000_000_000m);

        try
        {
            var tx = new TransactionBlock();

            tx.SetSender(sender.SuiAddress());
            tx.SetGasBudget(5000000);

            var amountArg = tx.AddPure(new U64(amountMist));

            var splitCoinArg = tx.AddSplitCoinsTx(tx.gas, new[] { amountArg }).ToArray();

            var recipientArg = tx.AddPure(receiver.SuiAddress());

            tx.AddTransferObjectsTx(splitCoinArg, recipientArg);

            var options = new TransactionBlockResponseOptions { ShowEffects = true };
            var result = await client.SignAndExecuteTransactionBlockAsync(tx, sender, options);

            if (result?.Result != null && result.Result.Effects != null)
            {
                var statusObj = result.Result.Effects.Status;

                // FIX: So sánh trực tiếp với Enum ExecutionStatusType.Success
                if (statusObj.Status == ExecutionStatus.Success)
                {
                    Debug.Log($"<color=green>Chuyển thành công!</color> Digest: {result.Result.Digest}");
                }
                else
                {
                    // Nếu thất bại, in ra lỗi chi tiết từ Node
                    Debug.LogError($"Blockchain từ chối: {statusObj.Error}");
                }
            }
            else if (result?.Error != null)
            {
                Debug.LogError($"Lỗi RPC: {result.Error.Message}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void IncreaseSuiCreate()
    {
        suiToCreate += 0.005 / 5;
    }
    public void ResetSuiAmount()
    {
        suiToCreate = 0;
        suiToReset += 0.003/5;
    }
    public void RerollSuiAmount()
    {
        suiToReroll += 0.0015 * 3/5;
    }
    public double TakeSui(int gameState)
    {
        if (gameState == 1) //GameOver
        {
            return suiToCreate + suiToReset + suiToReroll;
        }
        else if(gameState == 2) // Reroll Enemy
        {
            return suiToReroll;
        }
        else //Game Win
        {
            return suiToCreate;
        }
    }
}
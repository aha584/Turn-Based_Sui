using Sui.Accounts;
using Sui.Rpc; // Namespace thường dùng, hãy kiểm tra lại trong project của bạn
using Sui.Rpc.Client;
using Sui.Types;
using UnityEngine;
using System.Threading.Tasks;

public class SuiManager : MonoBehaviour
{
    public static SuiManager Instance;

    public SuiClient client;
    public string address;
    public double suiCoins;
    [SerializeField] private string mainNet = "https://fullnode.mainnet.sui.io:443";
    [SerializeField] private string devNet = "https://fullnode.devnet.sui.io:443";
    [SerializeField] private string testNet = "https://fullnode.testnet.sui.io:443";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetUpSlient();
    }

    private void SetUpSlient()
    {
        // 1. Khởi tạo SuiClient. 
        // Bạn cần truyền vào URL RPC của mạng (Mainnet, Testnet, hoặc Devnet).
        // Ví dụ RPC Mainnet: "https://fullnode.mainnet.sui.io:443"
        //Testnet: https://fullnode.testnet.sui.io:443
        //Devnet: https://fullnode.devnet.sui.io:443
        var connection = new Connection(devNet);
        client = new SuiClient(connection);
    }

    // Hàm lấy số dư
    public async Task GetBalance(string address)
    {
        // 2. Định nghĩa loại Coin bạn muốn kiểm tra. 
        // Mặc định SUI token là "0x2::sui::SUI".
        string coinType = "0x2::sui::SUI";

        AccountAddress testAddress = new AccountAddress(address);
        SuiStructTag coin_Type = new(coinType);

        // 3. Gọi hàm GetBalanceAsync
        var result = await client.GetBalanceAsync(testAddress, coin_Type);

        // 4. Xử lý kết quả trả về
        if (result.Error == null)
        {
            // Kết quả trả về nằm trong result.Result
            // TotalBalance thường ở dạng string hoặc BigInt (đơn vị MIST), cần chia cho 10^9 để ra SUI
            double mist = (double)(result.Result.TotalBalance);
            suiCoins = mist / Mathf.Pow(10, 9);

            //Debug.Log($"Balance của ví {address}: {suiCoins} SUI");
        }
        else
        {
            Debug.LogError($"Lỗi khi lấy balance: {result.Error.Message}");
        }
    }
    public async Task TransferSui()
    {
    }
}
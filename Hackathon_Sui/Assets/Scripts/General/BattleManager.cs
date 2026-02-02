using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public PlayerStatus playerPet;
    public EnemyStatus enemy;

    public Button attackButton;
    public Button blockButton;
    public HealthBar playerHealthBar;
    public HealthBar enemyHealthBar;
    public TMP_Text playerName;
    public TMP_Text enemyName;

    private int turnToDefeat = 5;
    private int currentTurn;
    private int currentOrderIndex;
    private List<BasicStatus> turnOrder = new();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        currentTurn = 0;
        currentOrderIndex = 0;
        if (VersusInfo.Instance == null) return;
        playerPet = VersusInfo.Instance.playerPet;
        playerPet.onDead += GameOver;
        playerHealthBar.myStatus = playerPet;
        playerName.text = $"{playerPet.myName} Lv.{playerPet.Level}";
        playerName.ForceMeshUpdate();
        enemy = VersusInfo.Instance.enemy;
        enemy.onDead += GameWin;
        enemyHealthBar.myStatus = enemy;
        enemyName.text = $"{enemy.myName} Lv.{enemy.Level}";
        enemyName.ForceMeshUpdate();

        turnOrder.Clear();
        turnOrder.Add(playerPet);
        turnOrder.Add(enemy);
        turnOrder = turnOrder.OrderByDescending(x => x.baseSpeed).ToList();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnOrder[currentOrderIndex] is PlayerStatus)
        {
            attackButton.interactable = true;
            blockButton.interactable = true;
            TurnInfo.Instance.dealer = playerPet;
            TurnInfo.Instance.receiver = enemy;
        }
        else
        {
            attackButton.interactable = false;
            blockButton.interactable = false;
            TurnInfo.Instance.dealer = enemy;
            TurnInfo.Instance.receiver = playerPet;
        }
        if(currentTurn > turnToDefeat)
        {
            playerPet.onDead?.Invoke();
        }
    }
    public void InscreaseIndex()
    {
        currentOrderIndex++;
        if(currentOrderIndex > turnOrder.Count)
        {
            currentOrderIndex = 0;
            IncreaseTurn();
        }
    }
    public void IncreaseTurn()
    {
        currentTurn++;
    }
    private void GameWin()
    {
        SceneManager.LoadScene("Choose Floor");
    }
    private void GameOver()
    {

    }
}

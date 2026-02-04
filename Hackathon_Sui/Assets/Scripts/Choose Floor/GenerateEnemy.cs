using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;

public class GenerateEnemy : MonoBehaviour
{
    public static GenerateEnemy Instance;

    public Image floor11;
    public Image floor12;
    public Image floor13;
    public TMP_Text chooseFloorText;

    public List<EnemyStatus> randomEnemy = new();
    public int currentFloorIndex = 1;

    private const int statPoints = 220;
    private int firstFloorIndex = 1;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chooseFloorText.text = $"Choose your opponent for floor {currentFloorIndex}";
        chooseFloorText.ForceMeshUpdate();
        Generate();
    }
    public void Generate()
    {
        randomEnemy.Clear();
        for (int i = 0; i < 3; i++)
        {
            int midPoint = 220 / 4;
            int baseHP = Random.Range(midPoint - 20, midPoint);
            int baseATK = Random.Range(midPoint - 20, midPoint);
            int baseDEF = Random.Range(midPoint - 20, midPoint);
            int baseSPD = Random.Range(midPoint - 20, midPoint);
            EnemyStatus enemy = new EnemyStatus($"Enemy {i}", baseHP, baseATK, baseDEF, baseSPD, 1);
            int spriteRandIndex = Random.Range(0, 8);
            //Debug.Log(spriteRandIndex);
            enemy.avatar = LoadAvatarByAddressables.Instance.avatarPrefabs[spriteRandIndex];
            randomEnemy.Add(enemy);
        }
        floor11.sprite = randomEnemy[0].avatar;
        floor12.sprite = randomEnemy[1].avatar;
        floor13.sprite = randomEnemy[2].avatar;
    }
}

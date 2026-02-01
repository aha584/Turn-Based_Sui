using UnityEngine;
using System;
using UnityEngine.UI;

public class BasicStatus : MonoBehaviour
{
    public string myName;
    public float baseHealthPoint;
    public float baseAttack;
    public float baseDefend;
    public float baseSpeed;
    public int level;

    public float currentHealth;
    public bool isBlock = false;
    public Action onDead;
    public Action onHealthChange;

    private float damageRedutionPercent = 0.65f;
    private float defendMultiplier = 1.15f;

    private void Awake()
    {
        //not equal
        //its involve level too
        currentHealth = baseHealthPoint;
    }

    public void TakeDamage(BasicStatus opponent, int isDealDamage)
    {
        if (!opponent.isBlock)
        {   //Not BaseAtk and BaseDef
            float dmgReceive = isDealDamage * (opponent.baseAttack - baseDefend);
            if (dmgReceive >= 0)
            {
                currentHealth -= dmgReceive;
                onHealthChange?.Invoke();
            }
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                onDead?.Invoke();
            }
        }
        else
        {
            float dmgReceive = isDealDamage * damageRedutionPercent * (opponent.baseAttack - defendMultiplier * baseDefend);
            if (dmgReceive >= 0)
            {
                currentHealth -= dmgReceive;
                onHealthChange?.Invoke();
            }
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                onDead?.Invoke();
            }
        }
        BattleManager.Instance.InscreaseIndex();
    }
}

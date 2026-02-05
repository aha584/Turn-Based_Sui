using UnityEngine;
using System;
using UnityEngine.UI;

[System.Serializable]
public class BasicStatus
{
    public string myName;
    public float baseHealthPoint;
    public float baseAttack;
    public float baseDefend;
    public float baseSpeed;
    public int Level;
    public Sprite avatar;

    public float currentHealth;
    public bool isBlock = false;
    public Action onDead;
    public Action onHealthChange;

    private float damageRedutionPercent = 0.15f;
    private float defendMultiplier = 1f;

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
            float dmgReceive = isDealDamage * (1f - damageRedutionPercent) * (opponent.baseAttack - defendMultiplier * baseDefend);
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

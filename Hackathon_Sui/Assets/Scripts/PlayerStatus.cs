using UnityEngine;
[System.Serializable]
public class PlayerStatus : BasicStatus
{
    public PlayerStatus(string name, float baseHP, float baseATK, float baseDEF, float baseSPD, int level)
    {
        myName = name;
        baseHealthPoint = baseHP;
        baseAttack = baseATK;
        baseDefend = baseDEF;
        baseSpeed = baseSPD;
        Level = level;
        currentHealth = baseHealthPoint;
    }
}

using UnityEngine;
using Random = UnityEngine.Random;
[System.Serializable]
public class EnemyStatus : BasicStatus
{
    public EnemyStatus(string name, float baseHP, float baseATK, float baseDEF, float baseSPD, int level)
    {
        myName = name;
        baseHealthPoint = baseHP;
        baseAttack = baseATK;
        baseDefend = baseDEF;
        baseSpeed = baseSPD;
        Level = level;
        currentHealth = baseHealthPoint;
    }
    public bool RandomAction()
    {
        int randNumber = Random.Range(0, 100);
        if(randNumber % 2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

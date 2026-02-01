using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStatus : BasicStatus
{
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

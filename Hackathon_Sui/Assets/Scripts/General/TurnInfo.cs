using UnityEngine;

public class TurnInfo : MonoBehaviour
{
    public static TurnInfo Instance;

    public BasicStatus dealer;
    public BasicStatus receiver;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance= this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dealer == null) return;
        if(receiver == null) return;

        if(dealer is EnemyStatus enemy)
        {
            if (enemy.RandomAction())
            {
                Attack();
            }
            else
            {
                Block();
            }
        }
    }

    private void Attack()
    {
        dealer.isBlock = false;
        receiver.TakeDamage(dealer, 1);
    }
    private void Block()
    {
        dealer.isBlock = true;
        receiver.TakeDamage(dealer, 0);
    }
}

using UnityEngine;
using TMPro;

public class TurnInfo : MonoBehaviour
{
    public static TurnInfo Instance;

    public BasicStatus dealer;
    public BasicStatus receiver;

    public TMP_Text dialog;

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

    public void Attack()
    {
        Debug.Log("Attack");
        dialog.text = $"{dealer.myName} use Attack on {receiver.myName}.";
        dialog.ForceMeshUpdate();
        dealer.isBlock = false;
        receiver.TakeDamage(dealer, 1);
    }
    public void Block()
    {
        Debug.Log("Blocks");
        dialog.text = $"{dealer.myName} use Block.";
        dialog.ForceMeshUpdate();
        dealer.isBlock = true;
        receiver.TakeDamage(dealer, 0);
    }
}

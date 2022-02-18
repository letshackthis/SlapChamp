using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SlapController player;
    [SerializeField] private GameManager gameManager;
    public void EnemySlap()
    {
        Debug.Log("EnemySlap");
        StartCoroutine(player.EnemySlapping());
    }

    public void PlayerGetHit()
    {
        player.GetHit("player");
    }

    public void StayDown()
    {
        player.StayDown("enemy");

    }
}

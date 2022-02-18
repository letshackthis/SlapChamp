using System.Collections.Generic;
using UnityEngine;

public class GamePlayPlace : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private List<HumanController> peopleList;

    private void Start()
    {
        transform.position = Player.transform.position;
        transform.eulerAngles =new Vector3(0,-Camera.main.transform.eulerAngles.y/2f,0) ;
        
        foreach (HumanController human in peopleList)
        {
            human.transform.LookAt(transform);
        }
    }

    public void Good()
    {
        foreach (HumanController human in peopleList)
        {
            human.SetRandomGood();
        }
    }

    public void Bad()
    {
        foreach (HumanController human in peopleList)
        {
            human.SetRandomBad();
        }
    }
}

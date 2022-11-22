using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStartPlayer : MonoBehaviour
{
    public bool StartMove;

    [SerializeField] Transform startpoint;



    public void GamePlayerStartMove()
    {
        StartMove = true;
    }


    private void Update()
    {
        if (StartMove)
        {
            MovePlayer();
        }
    }


    void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, startpoint.position, 1f);

        if (transform.position == startpoint.position)
        {
            StartMove = false;
        }
    }
}

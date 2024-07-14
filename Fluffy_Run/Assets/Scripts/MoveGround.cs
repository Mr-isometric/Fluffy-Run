using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField] private DataHolder_SO dataHolder_SO;
    private float Speed_Increase_Timer = 10f;

    private void Start()
    {
        dataHolder_SO.PlayerMoveSpeed = 10f;
    }
    private void Update()
    {
        SpeedIncreaseTimeHandler();
        MoveTheGround();
        
    }
    private void SpeedIncreaseTimeHandler()
    {
        Speed_Increase_Timer -= Time.deltaTime;
        if (Speed_Increase_Timer < 0f)
        {
            Speed_Increase_Timer = 6f;
            dataHolder_SO.PlayerMoveSpeed += 4f;
        }
    }
    private void MoveTheGround()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position += Vector3.back * dataHolder_SO.PlayerMoveSpeed * Time.deltaTime;
        }
    }
}

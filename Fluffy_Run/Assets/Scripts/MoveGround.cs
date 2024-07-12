using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField] private float GroundMoveSpeed;
    private void Update()
    {
        for(int i = 0;i<transform.childCount;i++)
        {
            transform.GetChild(i).position += Vector3.back * GroundMoveSpeed * Time.deltaTime;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Obstacle;
    private int SelectedObstacle;
    private Vector3 SpawnPos;
    private void Awake()
    {
        SelectedObstacle = Random.Range(0, 3);
    }
    private void Start()
    {
        switch (SelectedObstacle)
        {
            case 0:
                switch(Random.Range(0, 4))
                {
                    case 0:
                        SpawnPos = new Vector3(-3f, 0f, transform.position.z);
                        break;
                    case 1:
                        SpawnPos = new Vector3(-1f, 0f, transform.position.z);
                        break;
                    case 2:
                        SpawnPos = new Vector3(1f, 0f, transform.position.z);
                        break;
                    case 3:
                        SpawnPos = new Vector3(3f, 0f, transform.position.z);
                        break;
                }              
                break;
            case 1:
                SpawnPos = new Vector3(Random.Range(-1f, 1f) > 0 ? 2 : -2, 0f, transform.position.z);
                break;
            case 2:
                SpawnPos = new Vector3(0f, 0f, transform.position.z);
                break;

        }
        if (Random.Range(-1f, 1f) > 0 ? true : false)
        {
            
            Instantiate(Obstacle[SelectedObstacle], SpawnPos, Quaternion.identity, transform);
        }
    }

}

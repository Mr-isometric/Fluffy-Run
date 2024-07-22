using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _groundPrefab;
    [SerializeField] private Transform _Player;

    [SerializeField] private Transform _PlayerFront;
    [SerializeField] private Transform _PlayerBack;

    [SerializeField] private Transform _GroundFront;
    [SerializeField] private Transform _GroundBack;

    [SerializeField] private Transform CurrentGround;
    private void Update()
    {
        Vector3 playerPosition = _Player.position;

        if (_PlayerFront.position.z > _GroundFront.position.z)
        {
            //Spawn Ground
            SpawnGround();
        }
        if (_PlayerBack.position.z > transform.GetChild(0).GetChild(0).GetChild(1).position.z)
        {
            //Delete Ground
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    private void SpawnGround()
    {
        CurrentGround = Instantiate(_groundPrefab, CurrentGround.position + new Vector3(0f,0f,50f), Quaternion.identity,transform).transform;
        _GroundFront = CurrentGround.GetChild(0).GetChild(0);
        _GroundBack = CurrentGround.GetChild(0).GetChild(1);
    }
}

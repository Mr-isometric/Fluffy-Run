using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] Transform _playerCameraPosition;
    [SerializeField] private Vector3 CameraOffset;

    private void Update()
    {
        transform.position = new Vector3(_playerCameraPosition.position.x, 8f,_playerCameraPosition.position.z);
    }
}

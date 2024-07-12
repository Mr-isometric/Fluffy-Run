using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 CameraOffset;

    private void Start()
    {
        CameraOffset = player.transform.position-transform.position;
    }
    private void Update()
    {
        Vector3 Pos = new Vector3(0, 8, player.transform.position.z-CameraOffset.z);
        transform.position = Pos;
    }
}

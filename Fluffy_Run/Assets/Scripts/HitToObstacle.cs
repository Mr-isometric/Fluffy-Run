using UnityEngine;

public class HitToObstacle : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private DataHolder_SO dataHolder;
    private void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, 1f, layerMask);
        if (colliders.Length > 0)
        {
            dataHolder.isPlayerAlive = false;
            dataHolder.PlayerMoveSpeed = 0;
            UI_Manager.instance.PlayerGotHIT();
        }
    }
}

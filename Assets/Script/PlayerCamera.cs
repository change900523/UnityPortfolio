using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = Vector3.zero;

    private Transform player = null;

    public void Initialize(Transform inPlayer)
    {
        player = inPlayer;
        transform.position = player.position + offset;
        transform.LookAt(player);
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}

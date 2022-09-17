using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    Vector3 NewPosition;
    void FixedUpdate()
    {
        NewPosition = new(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        transform.position = NewPosition;
    }
}

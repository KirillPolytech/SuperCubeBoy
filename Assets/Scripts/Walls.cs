using UnityEngine;
[ExecuteInEditMode]
public class Walls : MonoBehaviour
{
    void Update()
    {
        transform.position = new(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
    }
}

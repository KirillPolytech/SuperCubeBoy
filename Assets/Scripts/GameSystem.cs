using UnityEngine;
public class GameSystem : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}

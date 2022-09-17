using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public static int index = 1;
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.gameObject.CompareTag("Player"))
        //{
            SceneManager.LoadScene(index);
            index++;
        //}
        Debug.Log(index);
    }
}

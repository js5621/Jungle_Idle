using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartControllder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartControllder : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
}

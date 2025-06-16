using UnityEngine;
using UnityEngine.SceneManagement;
public class StartSceneController : MonoBehaviour
{ 
    public void GotoLogin()
    {
        SceneManager.LoadScene(1);
    }
}

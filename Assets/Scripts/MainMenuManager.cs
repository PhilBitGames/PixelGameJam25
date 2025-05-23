using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}

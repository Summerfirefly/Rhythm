using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneUI : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}

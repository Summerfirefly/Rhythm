using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneUI : MonoBehaviour
{
    public void Start()
    {
        float width = Screen.width;
        float height = Screen.height;

        int fontSize = (int)(height / 15);
        float yOffset = height / 4;
        Vector2 rectSize = new Vector2(width / 3, height / 6);

        GameObject startGame = GameObject.Find("StartGame");
        GameObject settings = GameObject.Find("Settings");
        GameObject exit = GameObject.Find("Exit");

        startGame.GetComponent<RectTransform>().sizeDelta = rectSize;
        startGame.GetComponent<RectTransform>().localPosition = new Vector3(0, yOffset);
        startGame.GetComponentInChildren<Text>().fontSize = fontSize;

        settings.GetComponent<RectTransform>().sizeDelta = rectSize;
        settings.GetComponentInChildren<Text>().fontSize = fontSize;

        exit.GetComponent<RectTransform>().sizeDelta = rectSize;
        exit.GetComponentInChildren<Text>().fontSize = fontSize;
        exit.GetComponent<RectTransform>().localPosition = new Vector3(0, -yOffset);
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnSettingsClick()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}

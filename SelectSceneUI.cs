using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneUI : MonoBehaviour
{
    public RawImage coverImg;
    public Text songName;
    public Button prev;
    public Button next;
    public Button confirm;
    public Button back;

    static string[] list;

    void Start()
    {
        float imgWidth = Screen.height / 2.5f;
        float btnWidth = Screen.height / 6;

        prev.GetComponent<RectTransform>().sizeDelta = new Vector2(btnWidth, btnWidth);
        next.GetComponent<RectTransform>().sizeDelta = new Vector2(btnWidth, btnWidth);
        back.GetComponent<RectTransform>().sizeDelta = new Vector2(btnWidth, btnWidth);
        confirm.GetComponent<RectTransform>().sizeDelta = new Vector2(imgWidth, imgWidth / 5);
        confirm.GetComponentInChildren<Text>().fontSize = (int)(imgWidth / 15);
        coverImg.GetComponent<RectTransform>().sizeDelta = new Vector2(imgWidth, imgWidth);

        prev.GetComponent<RectTransform>().localPosition = new Vector3(-imgWidth / 2 - imgWidth / 4, 0);
        next.GetComponent<RectTransform>().localPosition = new Vector3(imgWidth / 2 + imgWidth / 4, 0);
        back.GetComponent<RectTransform>().localPosition = new Vector3(btnWidth - Screen.width / 2, Screen.height / 2 - btnWidth);
        confirm.GetComponent<RectTransform>().localPosition = new Vector3(0, -imgWidth / 2 - imgWidth / 4);

        songName.fontSize = (int)(imgWidth / 15);
        songName.GetComponent<RectTransform>().localPosition = new Vector3(0, imgWidth / 2 + imgWidth / 4);

        list = Resources.Load<TextAsset>("list").text.Split('\n');
        UpdateUI();
    }

    public void Prev()
    {
        GameStatus.currentSelect -= 1;
        if (GameStatus.currentSelect < 0)
        {
            GameStatus.currentSelect = list.Length - 1;
        }

        UpdateUI();
    }

    public void Next()
    {
        GameStatus.currentSelect += 1;
        if (GameStatus.currentSelect >= list.Length)
        {
            GameStatus.currentSelect = 0;
        }

        UpdateUI();
    }

    public void StartClick()
    {
        GameStatus.playName = list[GameStatus.currentSelect].Split(',')[0];
        SceneManager.LoadScene("PlayScene");
    }

    public void BackClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void UpdateUI()
    {
        string[] item = list[GameStatus.currentSelect].Split(',');
        songName.text = $"{item[1]} {item[2]}";
        coverImg.texture = Resources.Load<Texture>($"cover/{item[0]}");
    }
}

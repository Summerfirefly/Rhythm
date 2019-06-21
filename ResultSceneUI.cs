using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneUI : MonoBehaviour
{
    public Text perfect;
    public Text great;
    public Text good;
    public Text miss;

    public Button back;

    void Start()
    {
        float yUnit = Screen.height / 9;

        back.GetComponent<RectTransform>().sizeDelta = new Vector2(yUnit * 5.0f, yUnit);

        perfect.GetComponent<RectTransform>().localPosition = new Vector2(0, yUnit * 2.0f);
        great.GetComponent<RectTransform>().localPosition = new Vector2(0, yUnit * 1.0f);
        good.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        miss.GetComponent<RectTransform>().localPosition = new Vector2(0, yUnit * -1.0f);
        back.GetComponent<RectTransform>().localPosition = new Vector2(0, yUnit * -2.0f);

        perfect.fontSize = (int)(yUnit / 3);
        great.fontSize = (int)(yUnit / 3);
        good.fontSize = (int)(yUnit / 3);
        miss.fontSize = (int)(yUnit / 3);
        back.GetComponentInChildren<Text>().fontSize = (int)(yUnit / 3);

        perfect.text = $"Perfect: {GameStatus.perfect}";
        great.text = $"Great: {GameStatus.great}";
        good.text = $"Good: {GameStatus.good}";
        miss.text = $"Miss: {GameStatus.miss}";
    }

    public void BackClick()
    {
        GameStatus.comboNum = 0;
        GameStatus.perfect = 0;
        GameStatus.great = 0;
        GameStatus.good = 0;
        GameStatus.miss = 0;

        SceneManager.LoadScene("SelectScene");
    }
}

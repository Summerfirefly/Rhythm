using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsSceneUI : MonoBehaviour
{
    public Button speedUp;
    public Button speedDwon;
    public Button offsetUp;
    public Button offsetDown;
    public Button back;

    public Text speed;
    public Text offset;

    void Start()
    {
        GlobalData.LoadConfig();

        float length = Screen.height / 6;
        float yOffset = Screen.height / 10;
        float xOffset = Screen.width / 6;

        speedUp.GetComponent<RectTransform>().localPosition = new Vector3(xOffset, yOffset);
        speedDwon.GetComponent<RectTransform>().localPosition = new Vector3(-xOffset, yOffset);
        offsetUp.GetComponent<RectTransform>().localPosition = new Vector3(xOffset, -yOffset);
        offsetDown.GetComponent<RectTransform>().localPosition = new Vector3(-xOffset, -yOffset);
        back.GetComponent<RectTransform>().localPosition = new Vector3(length - Screen.width / 2, Screen.height / 2 - length);

        speedUp.GetComponent<RectTransform>().sizeDelta = new Vector2(length, length);
        speedDwon.GetComponent<RectTransform>().sizeDelta = new Vector2(length, length);
        offsetUp.GetComponent<RectTransform>().sizeDelta = new Vector2(length, length);
        offsetDown.GetComponent<RectTransform>().sizeDelta = new Vector2(length, length);
        back.GetComponent<RectTransform>().sizeDelta = new Vector2(length, length);

        speed.text = GlobalData.speedMul.ToString();
        speed.fontSize = (int)(length / 3);
        speed.GetComponent<RectTransform>().localPosition = new Vector3(0, yOffset);
        offset.text = GlobalData.offset.ToString();
        offset.fontSize = (int)(length / 3);
        offset.GetComponent<RectTransform>().localPosition = new Vector3(0, -yOffset);
    }

    void Update()
    {
        speed.text = $"速度: {GlobalData.speedMul.ToString()}%";
        offset.text = $"偏移: { GlobalData.offset.ToString()}";
    }

    public void SpeedUp()
    {
        GlobalData.speedMul += 10;
    }

    public void SpeedDown()
    {
        GlobalData.speedMul -= 10;
        if (GlobalData.speedMul < 10)
        {
            GlobalData.speedMul = 10;
        }
    }

    public void OffsetUp()
    {
        GlobalData.offset += 1;
    }

    public void OffsetDown()
    {
        GlobalData.offset -= 1;
    }

    public void Back()
    {
        GlobalData.speed = GlobalData.baseSpeed * GlobalData.speedMul / 100.0f;
        SceneManager.LoadScene("MenuScene");

        StreamWriter sw = new StreamWriter(new FileStream($"{Application.persistentDataPath}/settings.txt", FileMode.Create));
        sw.WriteLine($"speed,{GlobalData.speedMul}");
        sw.WriteLine($"offset,{GlobalData.offset}");
        sw.Close();
    }
}

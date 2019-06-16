using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GlobalData : MonoBehaviour
{
    public static RawImage noteTemplate;
    public static RawImage judgePoint;
    public static RawImage longPressTemplate;

    public static int colNum = 7;
    public static float interval = 1.5f;
    public static float leftNoteXPos = -(colNum / 2) * interval;
    public static float appearDistance = 3.8f;
    public static float tolerant = 0.2f;
    public static float judgeRange = 0.9f;

    public static float baseSpeed = 3.0f;
    public static float speed = 3.0f;
    public static int speedMul = 100;
    public static int offset = 0;

    void Awake()
    {
        noteTemplate = GameObject.Find("NoteTemplate").GetComponent<RawImage>();
        judgePoint = GameObject.Find("JudgePoint").GetComponent<RawImage>();
        longPressTemplate = GameObject.Find("LongPressTemplate").GetComponent<RawImage>();
        LoadConfig();
    }

    public static void LoadConfig()
    {
        string[] settings = Resources.Load<TextAsset>("settings").text.Split('\n');
        string settingFilePath = $"{Application.persistentDataPath}/settings.txt";

        if (File.Exists(settingFilePath))
        {
            StreamReader sr = new StreamReader(new FileStream(settingFilePath, FileMode.Open));
            settings = sr.ReadToEnd().Split('\n');
            sr.Close();
        }
        else
        {
            StreamWriter sw = new StreamWriter(new FileStream(settingFilePath, FileMode.Create));
            sw.Write(Resources.Load<TextAsset>("settings").text);
            sw.Close();
        }

        foreach (string kvpair in settings)
        {
            string[] str = kvpair.Split(',');
            switch (str[0])
            {
                case "speed":
                    speedMul = Convert.ToInt32(str[1]);
                    speed = baseSpeed * speedMul / 100.0f;
                    break;
                case "offset":
                    offset = Convert.ToInt32(str[1]);
                    break;
                default:
                    break;
            }
        }
    }
}

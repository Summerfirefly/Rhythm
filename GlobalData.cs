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
    public static float speed = 4.5f;
    public static float judgeRange = 1.0f;

    void Awake()
    {
        noteTemplate = GameObject.Find("NoteTemplate").GetComponent<RawImage>();
        judgePoint = GameObject.Find("JudgePoint").GetComponent<RawImage>();
        longPressTemplate = GameObject.Find("LongPressTemplate").GetComponent<RawImage>();
    }
}

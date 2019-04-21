using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static int colNum
    {
        get;
    } = 7;

    public static float interval = 1.5f;
    public static float leftNoteXPos = -(colNum / 2) * interval;
    public static float appearDistance = 3.8f;
    public static float speed = 3.0f;
}

using UnityEngine;
using UnityEngine.UI;

public enum Source
{
    ORIGIN = 0,
    NETWORK = 1
}

public class GameStatus : MonoBehaviour
{
    public static int nextNoteID = 0;
    public static int currentSelect = 0;
    public static string playName = "00001";
    public static Source source = Source.ORIGIN;
    public static float startTime;
    public static float length;

    public static int comboNum = 0;

    public static Transform slide;
    public static Text comboText;
}

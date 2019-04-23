using UnityEngine;
using UnityEngine.UI;

public enum NoteType
{
    NORMAL = 0,
    LEFT = 1,
    RIGHT = 2,
    LONG = 3
}

public class Note : MonoBehaviour
{
    public NoteType type;
    public bool active = false;
    public bool activated = false;
    public float activateTime = 0.0f;
    public float hitTime = 0.0f;

    // 激活Note，显示在窗口上并开始移动
    public void Activate()
    {
        if (activated) return;

        GetComponent<Transform>().SetParent(GameStatus.slide, true);
        active = true;
        activated = true;
        GameStatus.nextNoteID++;
        activateTime = (float)AudioSettings.dspTime;
    }

    // 生成Note，在第col列(从0开始计数)，类型为type，若为NoteType.LONG，则length表示长押持续时间
    public static Note CreateNote(int col, NoteType type, float length = 0)
    {
        RawImage noteObj;
        Note note;

        if (type == NoteType.LONG)
        {
            noteObj = Instantiate(GlobalData.longPressTemplate);
            noteObj.transform.position = new Vector3(GlobalData.leftNoteXPos + col * GlobalData.interval, GlobalData.appearDistance, 0);

            note = noteObj.gameObject.AddComponent<LongPressNote>();
            (note as LongPressNote).maxLength = length;
        }
        else
        {
            noteObj = Instantiate(GlobalData.noteTemplate);
            noteObj.transform.position = new Vector3(GlobalData.leftNoteXPos + col * GlobalData.interval, GlobalData.appearDistance, 0);

            note = noteObj.gameObject.AddComponent<SingleNote>();
        }

        note.type = type;
        return note;
    }


    // 生成Note，指定世界坐标系下X的值为xPosition，类型为type，若为NoteType.LONG，则length表示长押持续时间
    public static Note CreateNote(float xPosition, NoteType type, float length = 0)
    {
        RawImage noteObj;
        Note note;

        if (type == NoteType.LONG)
        {
            noteObj = Instantiate(GlobalData.longPressTemplate);
            noteObj.transform.position = new Vector3(xPosition, GlobalData.appearDistance, 0);

            note = noteObj.gameObject.AddComponent<LongPressNote>();
            (note as LongPressNote).maxLength = length;
        }
        else
        {
            noteObj = Instantiate(GlobalData.noteTemplate);
            noteObj.transform.position = new Vector3(xPosition, GlobalData.appearDistance, 0);

            note = noteObj.gameObject.AddComponent<SingleNote>();
        }

        note.type = type;
        return note;
    }

    // 使Note在窗口中消失，并不销毁
    public void Deactivate()
    {
        GetComponent<Transform>().SetParent(null, true);
        active = false;
    }

    protected virtual void OnStart()
    {
        switch (type)
        {
            case NoteType.LEFT:
                gameObject.GetComponent<RawImage>().texture = Resources.Load<Texture>("left");
                break;
            case NoteType.RIGHT:
                gameObject.GetComponent<RawImage>().texture = Resources.Load<Texture>("right");
                break;
            case NoteType.NORMAL:
                gameObject.GetComponent<RawImage>().texture = Resources.Load<Texture>("note");
                break;
            default:
                break;
        }
    }

    protected virtual void OnUpdate()
    {
        // 当前时刻(音乐开始播放时为0时刻) > note到达判定区的时刻 - note运动到判定区的时间
        if (AudioSettings.dspTime - GameStatus.startTime > hitTime - GlobalData.appearDistance / GlobalData.speed)
        {
            Activate();
        }
    }
}

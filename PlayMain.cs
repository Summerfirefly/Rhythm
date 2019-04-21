using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayMain : MonoBehaviour
{
    public RawImage noteTemplete;
    public RawImage judgePoint;

    private Transform slide;

    private float counter;
    private int nextNote;
    private int totalNum;
    private int debugCombo;

    private int windowHead;

    private float[] noteTimeline = new float[1024];
    private Note[] notes = new Note[1024];

    // Start is called before the first frame update
    void Start()
    {
        slide = GameObject.Find("Slide").GetComponent<Transform>();

        for (int i = 0; i < GlobalSettings.colNum; i++)
        {
            RawImage judgePointObj = Instantiate(judgePoint);
            judgePointObj.GetComponent<Transform>().SetParent(slide, true);
            judgePointObj.transform.position = new Vector3(-4.5f+i*1.5f, 0, 0);
        }

        Input.multiTouchEnabled = true;

        GameStatus.playName = "Roselia-Kimi no Kioku";

        // 初始化所有Note
        string[] score = Resources.Load<TextAsset>("score/Roselia-Kimi no Kioku").text.Split('\n');

        foreach (string scoreLine in score)
        {
            string[] temp = scoreLine.Split(',');
            noteTimeline[totalNum] = (float)Convert.ToDouble(temp[0]);
            notes[totalNum] = CreateNote(Convert.ToInt32(temp[1]));
            notes[totalNum].type = (NoteType)Convert.ToInt32(temp[2]);
            totalNum++;
        }

        // 播放音乐
        AudioSource music = Camera.main.gameObject.GetComponent<AudioSource>();
        music.clip = Resources.Load<AudioClip>("music/Roselia-Kimi no Kioku");
        music.Play();
    }


    void FixedUpdate()
    {
        // 显示应出现的Note
        while (nextNote < totalNum && Math.Abs(counter - noteTimeline[nextNote]) < 1e-5)
        {
            notes[nextNote].GetComponent<Transform>().SetParent(slide, true);
            notes[nextNote].active = true;
            nextNote++;
        }

        counter += Time.fixedDeltaTime;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Canceled) continue;

            Vector3 touchPosition = touch.position;
            touchPosition.z = 0.5f;

            for (int j = windowHead; j < nextNote; j++)
            {
                if ((Camera.main.ScreenToWorldPoint(touchPosition) - notes[j].gameObject.transform.position).magnitude < 1.0f)
                {
                    if (!notes[j].active) continue;

                    if (notes[j].type != NoteType.NORMAL)
                    {
                        if (notes[j].type == NoteType.LEFT && touch.deltaPosition.x > -2.0f) continue;
                        else if (notes[j].type == NoteType.RIGHT && touch.deltaPosition.x < 2.0f) continue;
                    }
                    else
                    {
                        if (touch.phase != TouchPhase.Ended) continue;
                    }

                    notes[j].GetComponent<Transform>().SetParent(null, true);
                    notes[j].active = false;
                    debugCombo++;
                    GameObject.Find("combo").GetComponent<Text>().text = debugCombo.ToString();
                    break;
                }
            }
        }
    }

    void LateUpdate()
    {
        while (windowHead < nextNote && !notes[windowHead].active)
        {
            windowHead++;
        }
    }

    Note CreateNote(int col)
    {
        RawImage noteObj = Instantiate(noteTemplete);
        noteObj.transform.position = new Vector3(GlobalSettings.leftNoteXPos + col * GlobalSettings.interval, GlobalSettings.appearDistance, 0);

        return noteObj.gameObject.AddComponent<Note>();
    }
}

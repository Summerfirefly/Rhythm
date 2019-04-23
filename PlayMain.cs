using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayMain : MonoBehaviour
{
    private int totalNum;
    private int debugCombo;

    private int windowHead;

    private float[] noteTimeline = new float[1024];
    private Note[] notes = new Note[1024];

    void Start()
    {
        GameStatus.slide = GameObject.Find("Slide").GetComponent<Transform>();

        for (int i = 0; i < GlobalData.colNum; i++)
        {
            RawImage judgePointObj = Instantiate(GlobalData.judgePoint);
            judgePointObj.GetComponent<Transform>().SetParent(GameStatus.slide, true);
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
            notes[totalNum] = Note.CreateNote(Convert.ToInt32(temp[1]), (NoteType)Convert.ToInt32(temp[2]), (float)Convert.ToDouble(temp[3]));
            notes[totalNum].hitTime = noteTimeline[totalNum];
            totalNum++;
        }

        // 播放音乐
        AudioSource music = Camera.main.gameObject.GetComponent<AudioSource>();
        music.clip = Resources.Load<AudioClip>("music/Roselia-Kimi no Kioku");
        GameStatus.startTime = (float)AudioSettings.dspTime;
        music.Play();
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Canceled) continue;

            Vector3 touchPosition = touch.position;
            touchPosition.z = 0.5f;

            for (int j = windowHead; j < GameStatus.nextNoteID; j++)
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

                    notes[j].Deactivate();
                    debugCombo++;
                    GameObject.Find("combo").GetComponent<Text>().text = debugCombo.ToString();
                    break;
                }
            }
        }
    }

    void LateUpdate()
    {
        while (windowHead < GameStatus.nextNoteID && !notes[windowHead].active)
        {
            windowHead++;
        }
    }
}

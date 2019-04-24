using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayMain : MonoBehaviour
{
    void Start()
    {
        GameStatus.slide = GameObject.Find("Slide").GetComponent<Transform>();
        GameStatus.comboText = GameObject.Find("ComboText").GetComponent<Text>();

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
            Note note;
            string[] temp = scoreLine.Split(',');
            note = Note.CreateNote(Convert.ToInt32(temp[1]), (NoteType)Convert.ToInt32(temp[2]), (float)Convert.ToDouble(temp[3]));
            note.hitTime = (float)Convert.ToDouble(temp[0]);
        }

        // 播放音乐
        AudioSource music = Camera.main.gameObject.GetComponent<AudioSource>();
        music.clip = Resources.Load<AudioClip>("music/Roselia-Kimi no Kioku");
        GameStatus.startTime = (float)AudioSettings.dspTime;
        music.Play();
    }

    void LateUpdate()
    {
        GameStatus.comboText.text = GameStatus.comboNum.ToString();
    }
}

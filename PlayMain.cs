using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMain : MonoBehaviour
{
    void Start()
    {
        GlobalData.LoadConfig();
        GameStatus.slide = GameObject.Find("Slide").GetComponent<Transform>();
        GameStatus.comboText = GameObject.Find("ComboText").GetComponent<Text>();

        float leftPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float rightPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        GlobalData.interval = (rightPos - leftPos) / (GlobalData.colNum + 1);
        GlobalData.leftNoteXPos = leftPos + GlobalData.interval;

        for (int i = 0; i < GlobalData.colNum; i++)
        {
            RawImage judgePointObj = Instantiate(GlobalData.judgePoint);
            judgePointObj.GetComponent<Transform>().SetParent(GameStatus.slide, true);
            judgePointObj.transform.position = new Vector3(GlobalData.leftNoteXPos + i * GlobalData.interval, 0, 0);
        }

        Input.multiTouchEnabled = true;

        // 初始化所有Note
        string[] score = Resources.Load<TextAsset>("score/" + GameStatus.playName).text.Split('\n');

        foreach (string scoreLine in score)
        {
            if (!scoreLine.Contains(","))
            {
                GameStatus.length = (float)Convert.ToDouble(scoreLine);
                continue;
            }

            Note note;
            string[] temp = scoreLine.Split(',');
            note = Note.CreateNote(Convert.ToInt32(temp[1]), (NoteType)Convert.ToInt32(temp[2]), (float)Convert.ToDouble(temp[3]));
            note.hitTime = (float)Convert.ToDouble(temp[0]) + GlobalData.offset / 20.0f;
        }

        // 播放音乐
        AudioSource music = Camera.main.gameObject.GetComponent<AudioSource>();
        music.clip = Resources.Load<AudioClip>("music/" + GameStatus.playName);
        GameStatus.startTime = (float)AudioSettings.dspTime;
        music.Play();
    }

    void LateUpdate()
    {
        GameStatus.comboText.text = GameStatus.comboNum.ToString();

        if (AudioSettings.dspTime - GameStatus.startTime > GameStatus.length)
        {
            AudioSource music = Camera.main.gameObject.GetComponent<AudioSource>();
            music.Stop();
            SceneManager.LoadScene("MenuScene");
        }
    }
}

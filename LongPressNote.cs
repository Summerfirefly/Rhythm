﻿using System;
using UnityEngine;

public class LongPressNote : Note
{
    public float holdTime = 0.0f;

    private float length = 0.0f;
    private float slideScaleY;

    private SingleNote headNote;
    private SingleNote tailNote;

    private bool holdVaild = false;

    void Start()
    {
        base.OnStart();

        headNote = CreateNote(colNum, NoteType.LONG_START) as SingleNote;
        tailNote = CreateNote(colNum, NoteType.LONG_END) as SingleNote;
        headNote.hitTime = hitTime;
        tailNote.hitTime = hitTime + holdTime;

        slideScaleY = GameObject.Find("Slide").GetComponent<RectTransform>().localScale.y;
    }

    void Update()
    {
        base.OnUpdate();

        if (active)
        {
            length = tailNote.transform.position.y - headNote.transform.position.y;

            if (length < -1.0f || (!headNote.active && headNote.activated) || (!tailNote.active && tailNote.activated))
            {
                headNote.Deactivate();
                tailNote.Deactivate();
                Deactivate();
                GameStatus.comboNum = 0;
            }

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Canceled) continue;

                Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (Math.Abs(touchWorldPosition.y) > GlobalData.judgeRange) continue;
                if (Math.Abs(tailNote.transform.position.x - touchWorldPosition.x) > GlobalData.interval / 2) continue;

                if (touch.phase == TouchPhase.Ended)
                {
                    float time = (float)Math.Abs(AudioSettings.dspTime - GameStatus.startTime - tailNote.hitTime);
                    if (time < GlobalData.tolerant)
                    {
                        GameStatus.comboNum++;

                        if (time < 0.07) GameStatus.perfect++;
                        else if (time > 0.14) GameStatus.good++;
                        else GameStatus.great++;
                    }
                    else if (!headNote.holding)
                    {
                        continue;
                    }
                    else
                    {
                        GameStatus.comboNum = 0;
                        GameStatus.miss++;
                    }

                    headNote.Deactivate();
                    tailNote.Deactivate();
                    Deactivate();
                    break;
                }
                else if (touch.phase == TouchPhase.Began && Math.Abs(AudioSettings.dspTime - GameStatus.startTime - headNote.hitTime) < GlobalData.tolerant)
                {
                    float time = (float)Math.Abs(AudioSettings.dspTime - GameStatus.startTime - headNote.hitTime);
                    headNote.transform.position = new Vector3(headNote.transform.position.x, 0, headNote.transform.position.z);
                    if (!headNote.holding)
                    {
                        GameStatus.comboNum++;
                        if (time < 0.07) GameStatus.perfect++;
                        else if (time > 0.14) GameStatus.good++;
                        else GameStatus.great++;
                    }
                    headNote.holding = true;
                    holdVaild = true;
                }
                else if (Math.Abs(tailNote.transform.position.x - touchWorldPosition.x) < GlobalData.interval / 2)
                {
                    holdVaild = true;
                }
            }

            if (active && !holdVaild && headNote.holding)
            {
                headNote.Deactivate();
                tailNote.Deactivate();
                Deactivate();
                GameStatus.comboNum = 0;
                GameStatus.miss++;
            }

            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, length / slideScaleY, 1f);
            gameObject.GetComponent<RectTransform>().position = (tailNote.transform.position + headNote.transform.position) / 2;
        }
    }

    void LateUpdate()
    {
        holdVaild = false;
    }
}

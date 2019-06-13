﻿using System;
using UnityEngine;

public class SingleNote : Note
{
    public bool holding = false;

    void Start()
    {
        base.OnStart();
    }

    void Update()
    {
        base.OnUpdate();

        if (active)
        {
            if (!holding)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    GlobalData.appearDistance - GlobalData.speed * (float)(AudioSettings.dspTime - activateTime),
                    transform.position.z);
            }

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Canceled) continue;

                Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (Math.Abs(touchWorldPosition.y) > GlobalData.judgeRange) continue;
                touchWorldPosition.z = 0;

                if (Math.Abs(AudioSettings.dspTime - GameStatus.startTime - hitTime) < 0.2f &&
                    Math.Abs(transform.position.x - touchWorldPosition.x) < GlobalData.interval / 2)
                {
                    if (type == NoteType.NORMAL)
                    {
                        if (touch.phase != TouchPhase.Began) continue;
                    }
                    else if (type == NoteType.LONG_START || type == NoteType.LONG_END)
                    {
                        continue;
                    }
                    else
                    {
                        if (type == NoteType.LEFT && touch.deltaPosition.x > -2.0f) continue;
                        else if (type == NoteType.RIGHT && touch.deltaPosition.x < 2.0f) continue;
                    }

                    Deactivate();
                    GameStatus.comboNum++;
                    break;
                }
            }

            if (transform.position.y < -GlobalData.judgeRange)
            {
                Deactivate();
                GameStatus.comboNum = 0;
            }
        }
    }
}

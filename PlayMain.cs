using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayMain : MonoBehaviour
{
    public RawImage noteTemplete;
    public RawImage judgePoint;

    private float counter;
    private int noteIndex;
    private int totalNum;
    private int debugCombo;
    private float[] noteTimeline = new float[1024];
    private Note[] notes = new Note[1024];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GlobalSettings.colNum; i++)
        {
            RawImage judgePointObj = Instantiate(judgePoint);
            judgePointObj.GetComponent<Transform>().SetParent(GameObject.Find("Slide").GetComponent<Transform>(), true);
            judgePointObj.transform.position = new Vector3(-4.5f+i*1.5f, 0, 0);
        }

        Input.multiTouchEnabled = true;

        GameStatus.playName = "Roselia-Kimi no Kioku";

        string[] score = Resources.Load<TextAsset>("score/Roselia-Kimi no Kioku").text.Split('\n');

        foreach (string scoreLine in score)
        {
            string[] temp = scoreLine.Split(',');
            noteTimeline[totalNum] = (float)Convert.ToDouble(temp[0]);
            notes[totalNum] = CreateNote(Convert.ToInt32(temp[1]));
            notes[totalNum].type = (NoteType)Convert.ToInt32(temp[2]);
            totalNum++;
        }
    }


    void FixedUpdate()
    {
        while (noteIndex < totalNum && Math.Abs(counter - noteTimeline[noteIndex]) < 1e-5)
        {
            notes[noteIndex].active = true;
            notes[noteIndex].GetComponent<Transform>().SetParent(GameObject.Find("Slide").GetComponent<Transform>(), true);
            noteIndex++;
        }

        counter += Time.fixedDeltaTime;

        if (Input.touchCount > 0)
        {
            Vector3 touchPosition = Input.touches[0].position;
            touchPosition.z = 0.5f;
            if ((Camera.main.ScreenToWorldPoint(touchPosition)-notes[GlobalSettings.closestNote].gameObject.transform.position).magnitude<0.7f)
            {
                Destroy(notes[GlobalSettings.closestNote].gameObject);
                debugCombo++;
                GameObject.Find("combo").GetComponent<Text>().text = debugCombo.ToString();
            }
        }
    }

    Note CreateNote(int col)
    {
        RawImage noteObj = Instantiate(noteTemplete);
        noteObj.transform.position = new Vector3(GlobalSettings.leftNoteXPos + col * GlobalSettings.interval, GlobalSettings.appearDistance, 0);
        Rigidbody rigidBody =  noteObj.gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;

        return noteObj.gameObject.AddComponent<Note>();
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayMain : MonoBehaviour
{
    public RawImage noteTemplete;
    public RawImage judgePoint;

    private float counter;
    private int noteIndex;
    private int debugCombo;
    private float[] noteList = {0.0f, 2.0f, 2.2f, 2.4f, 3.0f, 3.5f, 3.8f, 4.8f };
    private Note[] notes = new Note[8];

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
    }


    void FixedUpdate()
    {
        if (noteIndex < noteList.Length && Math.Abs(counter - noteList[noteIndex]) < 1e-5)
        {
            CreateNote(noteIndex % 7);
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

    void CreateNote(int col)
    {
        RawImage noteObj = Instantiate(noteTemplete);
        noteObj.GetComponent<Transform>().SetParent(GameObject.Find("Slide").GetComponent<Transform>(), true);
        noteObj.transform.position = new Vector3(GlobalSettings.leftNoteXPos + col * GlobalSettings.interval, 3.8f, 0);
        Rigidbody rigidBody =  noteObj.gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;

        notes[noteIndex] = noteObj.gameObject.AddComponent<Note>();
        notes[noteIndex].type = NoteType.NORMAL;
    }
}

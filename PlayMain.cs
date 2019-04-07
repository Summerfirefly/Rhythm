using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayMain : MonoBehaviour
{
    public RawImage noteTemplete;
    public RawImage horizonLine;
    public RawImage verticalLine;
    public RawImage slideBack;

    private float counter;
    private int noteIndex;
    private float[] noteList = {0.0f, 2.0f, 2.2f, 2.4f, 3.0f, 3.5f, 3.8f, 4.8f };
    private Note[] notes = new Note[8];

    // Start is called before the first frame update
    void Start()
    {
        RawImage horizonLineObj = Instantiate(horizonLine);
        horizonLineObj.GetComponent<Transform>().SetParent(GameObject.Find("Slide").GetComponent<Transform>(), true);
        horizonLineObj.transform.position = new Vector3(0, 0, 0);

        RawImage subslideObj = Instantiate(slideBack);
        subslideObj.GetComponent<Transform>().SetParent(GameObject.Find("Slide").GetComponent<Transform>(), true);
        subslideObj.transform.position = new Vector3(0, 0, 75f);

        for (int i = 0; i <= GlobalSettings.colNum; i++)
        {
            RawImage verticalLineObj = Instantiate(verticalLine);
            verticalLineObj.GetComponent<Transform>().SetParent(GameObject.Find("Slide").GetComponent<Transform>(), true);
            verticalLineObj.transform.position = new Vector3(-5.25f+i*1.5f, 0, 75f);
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
            touchPosition.z = 6.0f;
            if ((Camera.main.ScreenToWorldPoint(touchPosition)-notes[GlobalSettings.closestNote].gameObject.transform.position).magnitude<1.5f)
            {
                Destroy(notes[GlobalSettings.closestNote].gameObject);
            }
        }
    }

    void CreateNote(int col)
    {
        RawImage noteObj = Instantiate(noteTemplete);
        noteObj.GetComponent<Transform>().SetParent(GameObject.Find("Slide").GetComponent<Transform>(), true);
        noteObj.transform.position = new Vector3(GlobalSettings.leftNoteXPos + col * GlobalSettings.interval, 0, GlobalSettings.appearDistance);

        notes[noteIndex] = noteObj.gameObject.AddComponent<Note>();
        notes[noteIndex].type = NoteType.NORMAL;
    }
}

using UnityEngine;

public class LongPressNote : Note
{
    public float maxLength = 0.0f;

    private float length = 0.0f;
    private float slideScaleY;

    private Note headNote;
    private Note tailNote;

    void Start()
    {
        base.OnStart();

        headNote = CreateNote(transform.position.x, NoteType.NORMAL);
        tailNote = CreateNote(transform.position.x, NoteType.NORMAL);
        headNote.hitTime = hitTime;
        tailNote.hitTime = hitTime + maxLength;

        slideScaleY = GameObject.Find("Slide").GetComponent<RectTransform>().localScale.y;
    }

    void Update()
    {
        base.OnUpdate();

        if (active)
        {
            length = tailNote.transform.position.y - headNote.transform.position.y;

            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, length / slideScaleY, 1f);
            gameObject.GetComponent<RectTransform>().position = (tailNote.transform.position + headNote.transform.position) / 2;
        }
    }
}

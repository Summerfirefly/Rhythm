using UnityEngine;
using UnityEngine.UI;

public enum NoteType
{
    NORMAL = 0,
    LEFT = 1,
    RIGHT = 2
}

public class Note : MonoBehaviour
{
    public NoteType type;
    public bool active = false;

    void Start()
    {
        switch (type)
        {
            case NoteType.LEFT:
                gameObject.GetComponent<RawImage>().texture = Resources.Load<Texture>("left");
                break;
            case NoteType.RIGHT:
                gameObject.GetComponent<RawImage>().texture = Resources.Load<Texture>("right");
                break;
            case NoteType.NORMAL:
                gameObject.GetComponent<RawImage>().texture = Resources.Load<Texture>("note");
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        if (active)
        {
            Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
            gameObject.GetComponent<Rigidbody>().velocity = v + new Vector3(0, GlobalSettings.acclerateY, 0);
        }
    }

    void OnDestroy()
    {
        GlobalSettings.closestNote++;
    }
}

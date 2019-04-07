using UnityEngine;
using UnityEngine.UI;

public enum NoteType
{
    LEFT,
    RIGHT,
    NORMAL
}

public class Note : MonoBehaviour
{
    public NoteType type;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 oldPosition = gameObject.transform.position;
        gameObject.transform.position = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z - GlobalSettings.noteVelocity * Time.fixedDeltaTime);
    }

    void OnDestroy()
    {
        GlobalSettings.closestNote++;
    }
}

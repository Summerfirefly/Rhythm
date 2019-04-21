using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -2)
        {
            transform.SetParent(null, true);
            GetComponent<Note>().active = false;
        }
    }
}

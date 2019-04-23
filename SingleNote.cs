using UnityEngine;

public class SingleNote : Note
{
    void Start()
    {
        base.OnStart();
    }

    void Update()
    {
        base.OnUpdate();
        if (active)
        {
            transform.position = new Vector3(
                transform.position.x,
                GlobalData.appearDistance - GlobalData.speed * (float)(AudioSettings.dspTime - activateTime),
                transform.position.z
            );
        }
    }
}

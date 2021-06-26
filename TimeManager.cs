/*
How to use this script?

1. Create a gameobject in your scene and add this script to that gameobject OR add it one of your existing gameobjects in the scene. (I will suggest you add it to the managers gameobject.)
2. Adjust the slowdown length and slowdown as per your liking.
3. In order to start the slowdown effect just add this line of code to your script: "TimeManager.Instance.DoSlowmotion();"
4. Voila you have time control now.


Next steps:
If you want to take this a step further then turn this function call into an Event and invoke it from anywhere without explicitly using the TimeManager class,

Disclosure:
Some of this code from Brackey's video but I modified it a little so that it does not use the update function as that would impact your game's performance since we were doing
some floating point calculations each frame..

*/

using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    public float slowdownFactor = 0.1f;
    public float slowdownLength = 2f;

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void DoSlowmotion()
    {
        StartCoroutine(ExecuteTimeSlowmo());
    }

    // The coroutine will stop once it the timescale has reached 1.0f unlike an update method that runs each frame.
    IEnumerator ExecuteTimeSlowmo()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

        float currentTimeScale = slowdownFactor;

        while (currentTimeScale <= 1.0f)
        {
            currentTimeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = currentTimeScale;
            yield return null;
        }

        Time.timeScale = 1.0f;
    }

}

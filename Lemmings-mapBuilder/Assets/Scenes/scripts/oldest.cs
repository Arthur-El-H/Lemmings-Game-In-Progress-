using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldest : MonoBehaviour
{
    int idleCountdownTime = 20;
    int currentIdleCountdownTime;
    // Start is called before the first frame update
    void Start()
    {
        currentIdleCountdownTime = idleCountdownTime;
        StartCoroutine(idleCountdown());
    }

    public void talk(string message)
    {
        Debug.Log(message);
        currentIdleCountdownTime = idleCountdownTime;
    }

    public void getMessage(string message, int minSecOfLastMessage = 0)
    {
        minSecOfLastMessage = 20 - minSecOfLastMessage;
        if(!(minSecOfLastMessage < currentIdleCountdownTime))
        {
            talk(message);
        }
    }

    private string getRandomIdle()
    {
        return "test";
    }

    IEnumerator idleCountdown()
    {
        while (true)
        {
            if (currentIdleCountdownTime > 0) { currentIdleCountdownTime--; }
            if (currentIdleCountdownTime == 0) { talk(getRandomIdle() ); }
            yield return new WaitForSeconds(1);
        }
    }
}

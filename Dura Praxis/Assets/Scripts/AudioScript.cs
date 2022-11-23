using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    public AudioSource timer;
   

    // Update is called once per frame
    void timerCountDown()
    {
        timer.Play();
    }
}

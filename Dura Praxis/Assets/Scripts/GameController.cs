using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool startTime = false;

    [SerializeField] float timeCounter = 30f;
    [SerializeField] float timeOut = 0f;
    [SerializeField] Text timerText;


    // Start is called before the first frame update
    void Start()
    {
        startTime = true;

    }

    // Update is called once per frame
    void Update()
    {
    //    if (startTime == true)
    //    {
    //        timeCounter -= Time.deltaTime;
    //
    //        if (timeCounter <= timeOut)
    //        {
    //
    //          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    //
    //        }
    //    }

        //timerText.autoSizeTextContainer = true;
        timerText.text = (("Time Left: ") + (int)timeCounter + " sec").ToString();

    }
}

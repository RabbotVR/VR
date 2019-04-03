using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 300f;
    float timeLeft;
    //public GameObject timesUpText;
    public bool CountIsTrigger = false;


    void Start()
    {
       // timesUpText.SetActive(false);
        timerBar = GetComponent<Image> ();
        timeLeft = maxTime;
    }


    void Update()
    {
        if (CountIsTrigger == true){
            startCount();
        }
    }

    public void CountTrigger(){
        CountIsTrigger = true;
    }

    void startCount(){
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
           // timesUpText.SetActive(true);
            Time.timeScale = 0;
            SceneManager.LoadScene("EndScene");
        }
    }
}

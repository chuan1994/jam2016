using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeController : MonoBehaviour {


    public delegate void GameOverHandler(int score);
    public static event GameOverHandler gameOverHandler;

    [SerializeField]
    public float time_remaining;
    public bool gameOver;
    bool start;
    int score;
    Text timeTextScript;
    Text scoreTextScript;

    int minutes;
    int seconds;

    

	// Use this for initialization
	void Start () {
        timeTextScript = GameObject.Find("TimeText").GetComponent<Text>();
        scoreTextScript = GameObject.Find("ScoreText").GetComponent<Text>();
    }
    
    // Update is called once per frame
    void Update() {
        if (gameOver) return;
        string answer;
        time_remaining -= Time.deltaTime;
        if (start)
        {
            if (time_remaining > 0)
            {
                TimeSpan t = TimeSpan.FromSeconds(time_remaining);
                answer = string.Format("{0:D2}:{1:D2}",
                                t.Minutes,
                                t.Seconds);
            }
            else
            {
                Debug.Log("hi");
                time_remaining = 0;
                answer = "00:00";
                if (gameOverHandler != null)
                {
                    gameOverHandler(score);
                }

                gameOver = true;
            }


            if (timeTextScript != null)
            {
                timeTextScript.text = answer;
                scoreTextScript.text = "Score: " + score.ToString();
            }
        }

    }

    public void goodMove(int scores) {
        time_remaining = time_remaining + 5;
        score = scores;
    }

    public void badMove() {
        time_remaining = time_remaining - 10;
    }

    public void setTime(float tim) {
        time_remaining = tim;
        start = true;
    }
}

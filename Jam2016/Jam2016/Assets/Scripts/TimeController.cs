using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeController : MonoBehaviour {

    [SerializeField]
    public float time_remaining;

    Text textScript;

    int minutes;
    int seconds;

	// Use this for initialization
	void Start () {
        textScript = GetComponent<Text>(); 
	}
    
    // Update is called once per frame
    void Update() {

        string answer;
        time_remaining -= Time.deltaTime;
        if (time_remaining > 0)
        {
            TimeSpan t = TimeSpan.FromSeconds(time_remaining);
            answer = string.Format("{0:D2}:{1:D2}",
                            t.Minutes,
                            t.Seconds);
        }
        else
        {
            time_remaining = 0;
            answer = "00:00";
        }

        
        if (textScript != null)
        {
            textScript.text = answer;
        }
    }

    public void goodMove() {
        time_remaining = time_remaining + 5;
    }

    public void badMove() {
        time_remaining = time_remaining - 10;
    }
}

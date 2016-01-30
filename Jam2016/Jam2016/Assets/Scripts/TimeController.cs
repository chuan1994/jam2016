using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeController : MonoBehaviour {

    [SerializeField]
    float time_remaining;

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
            answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Minutes,
                            t.Seconds,
                            t.Milliseconds);
        }
        else
        {
            time_remaining = 0;
            answer = "Time's Up";
        }

        
        if (textScript != null)
        {
            textScript.text = answer;
        }


    }
}

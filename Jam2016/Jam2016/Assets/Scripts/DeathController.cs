using UnityEngine;
using System.Collections;

public class DeathController : MonoBehaviour {

    public delegate void restart();
    public static event restart sendEvent;

	// Use this for initialization
	void Start () {
        this.gameObject.AddComponent<AudioSource>();
        this.gameObject.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("WRONG");
        this.gameObject.GetComponent<AudioSource>().volume = 2;

        StartCoroutine("renableClicks");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator renableClicks(){
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        killObj();
    }

    void killObj() {
        if (sendEvent != null) {
            sendEvent();
        }
        Destroy(this.gameObject);
    }
}

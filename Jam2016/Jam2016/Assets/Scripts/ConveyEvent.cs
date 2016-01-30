using UnityEngine;
using System.Collections;

public class ConveyEvent : MonoBehaviour {

    public delegate void mousePressed(GameObject go);
    public static event mousePressed mousePressedEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() {
        if (mousePressedEvent != null) {
            mousePressedEvent(this.gameObject);
        }
    }
}

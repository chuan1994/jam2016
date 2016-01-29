using UnityEngine;
using System.Collections;

public class GeneralClickController : MonoBehaviour {

    public delegate void mousePressed(GameObject self);
    public static event mousePressed eventUponClick;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (eventUponClick != null)
        {
            eventUponClick(this.gameObject);
        }
    }
}

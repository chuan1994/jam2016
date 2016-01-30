﻿using UnityEngine;
using System.Collections;

public class GeneralClickController : MonoBehaviour {

    public delegate void mousePressed(GameObject self);
    public static event mousePressed eventUponClick;

	// Use this for initialization
	void Awake () {
        LevelManager.enableActions += EnableScript;
        LevelManager.disableActions += DisableScript;
	}
    
    void OnDestroy()
    {
        LevelManager.enableActions -= EnableScript;
        LevelManager.disableActions -= DisableScript;
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

    void EnableScript()
    {
        this.enabled = true;
    }

    void DisableScript()
    {
        this.enabled = false;
    }
}

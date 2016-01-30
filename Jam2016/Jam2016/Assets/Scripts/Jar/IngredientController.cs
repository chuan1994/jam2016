using UnityEngine;
using System.Collections;

public class IngredientController : MonoBehaviour {

    public delegate void IngredientMouseHandler(GameObject g);
    public static event IngredientMouseHandler JarHandler;

    public bool scriptEnabled;

	// Use this for initialization
	void Awake () {
        LevelManager.enableIngredients += EnableScript;
        LevelManager.disableIngredients += DisableScript;
        scriptEnabled = true;
	}

    void Update() { }
    
	// Update is called once per frame
	void OnDestroy () {
        LevelManager.enableIngredients -= EnableScript;
        LevelManager.disableIngredients -= DisableScript;
    }

    void OnMouseDown()
    {
        if (scriptEnabled)
        {
            if (JarHandler != null)
            {
                JarHandler(this.gameObject);
            }
        }
    }

    void EnableScript()
    {
        scriptEnabled = true;
    }

    void DisableScript()
    {
        scriptEnabled = false;
    }
}

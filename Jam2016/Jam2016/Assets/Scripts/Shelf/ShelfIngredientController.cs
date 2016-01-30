using UnityEngine;
using System.Collections;

public class ShelfIngredientController : MonoBehaviour {

    public delegate void ShelfMouseDownHandler(Vector3 pos);
    public static event ShelfMouseDownHandler ShelfMouseDown;

    public int index;

    public bool scriptEnabled;

    // Use this for initialization
    void Awake () {
        Debug.Log("subscribe");
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

    void EnableScript()
    {
        scriptEnabled = true;
    }

    void DisableScript()
    {
        scriptEnabled = false;
    }

    void OnMouseDown() {
        if (scriptEnabled)
        {
            if (ShelfMouseDown != null)
            {
                ShelfMouseDown(transform.position);
            }

            Trash();
        }
        
    }

    void Trash()
    {
        Destroy(gameObject);
    }
    




}

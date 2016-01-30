using UnityEngine;
using System.Collections;

public class ShelfIngredientController : MonoBehaviour {

    public delegate void ShelfMouseDownHandler(Vector3 pos);
    public static event ShelfMouseDownHandler ShelfMouseDown;

    public int index;

    // Use this for initialization
    void Start () {
        LevelManager.enableActions += EnableScript;
        LevelManager.disableScripts += DisableScript;
	}

    void OnDestroy()
    {
        LevelManager.enableActions -= EnableScript;
        LevelManager.disableScripts -= DisableScript;
    }

	// Update is called once per frame
	void Update () {


	
	}

    void EnableScript()
    {
        this.enabled = true;
    }

    void DisableScript()
    {
        this.enabled = false;
    }

    void OnMouseDown() {

        if (ShelfMouseDown != null)
        {
            ShelfMouseDown(transform.position);
        }

        Trash();
        
    }

    void Trash()
    {
        Destroy(gameObject);
    }
    




}

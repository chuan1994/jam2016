using UnityEngine;
using System.Collections;

public class ShelfIngredientController : MonoBehaviour {

    public delegate void ShelfMouseDownHandler(Vector3 pos);
    public event ShelfMouseDownHandler ShelfMouseDown;

    public int index;

    // Use this for initialization
    void Start () {

        	
	}
	
	// Update is called once per frame
	void Update () {


	
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

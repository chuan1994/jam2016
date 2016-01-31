using UnityEngine;
using System.Collections;

public class ShelfIngredientController : MonoBehaviour {

    public delegate void ShelfMouseDownHandler(Vector3 pos);
    public static event ShelfMouseDownHandler ShelfMouseDown;

    public int index;
    
    void OnDestroy()
    {
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

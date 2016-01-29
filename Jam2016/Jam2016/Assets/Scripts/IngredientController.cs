using UnityEngine;
using System.Collections;

public class IngredientController : MonoBehaviour {

    public delegate void IngredientMouseHandler(GameObject g);
    public static event IngredientMouseHandler JarHandler;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnMouseDown()
    {
        if (JarHandler != null)
        {
            JarHandler(this.gameObject);
        }
    }
}

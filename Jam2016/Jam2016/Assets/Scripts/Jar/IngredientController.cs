using UnityEngine;
using System.Collections;

public class IngredientController : MonoBehaviour {

    public delegate void IngredientMouseHandler(GameObject g);
    public static event IngredientMouseHandler JarHandler;

    GameObject blankIngredient;
    

    void Start()
    {
        //blankIngredient = GameObject.Find("SimpleCrate");
        blankIngredient = (GameObject)Resources.Load("SimpleCrate");

    }

    void Update() { }

    void OnMouseDown()
    {
        if (GetComponent<BaseIngredientController>().id > 2)
        {
            Instantiate(blankIngredient).transform.position = transform.position;
        }

        if (JarHandler != null)
        {
            if (GetComponent<ShelfIngredientController>() == null) {
                JarHandler(this.gameObject);
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class IngredientController : MonoBehaviour {

    public delegate void IngredientMouseHandler(GameObject g);
    public static event IngredientMouseHandler JarHandler;

    void Update() { }

    void OnMouseDown()
    {
            if (JarHandler != null)
            {
                if (GetComponent<ShelfIngredientController>() == null) {
                    JarHandler(this.gameObject);
                }
            }
    }
}

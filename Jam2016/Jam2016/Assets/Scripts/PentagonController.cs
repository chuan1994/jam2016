using UnityEngine;
using System.Collections;

public class PentagonController : MonoBehaviour {

    [SerializeField]
    float Speed;

    GameObject Ingredient;

    private bool move;

	// Use this for initialization
	void Start () {
        OnEnable();
        move = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (move)
        {
            if (Ingredient.transform.position != transform.position)
            {
                float step = Speed * Time.deltaTime;
                Ingredient.transform.position = Vector3.MoveTowards(Ingredient.transform.position, transform.position, step);
            }
            else
            {
                move = false;
            }
        }
    }

    void JarHandler(GameObject g)
    {
        Ingredient = g;
        move = true;
    }

    void OnEnable()
    {
        IngredientController.JarHandler += JarHandler;
    }

    void OnDisable()
    {
        IngredientController.JarHandler -= JarHandler;
    }
}

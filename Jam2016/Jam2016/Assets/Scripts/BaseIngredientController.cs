using UnityEngine;
using System.Collections;

public class BaseIngredientController : MonoBehaviour {

    public int id;

	// Use this for initialization
	void Awake () {
        LevelManager.enableActions += createActionScript;
        LevelManager.enableIngredients += createIngredientScript;
	}

    void Destroy()
    {
        LevelManager.enableActions -= createActionScript;
        LevelManager.enableIngredients -= createIngredientScript;
    }

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void createActionScript()
    {
        if (gameObject.GetComponent<IngredientController>() != null)
        {
            Destroy(gameObject.GetComponent<IngredientController>());
        }
        if (id > 2)
        {
            gameObject.AddComponent<ShelfIngredientController>();
        }
    }

    void createIngredientScript()
    {
        if (id > 2) {
            if (gameObject.GetComponent<ShelfIngredientController>() != null)
            {
                Destroy(gameObject.GetComponent<ShelfIngredientController>());
            }
        }
        gameObject.AddComponent<IngredientController>();
    }
}

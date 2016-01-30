using UnityEngine;
using System.Collections;

public class BaseIngredientController : MonoBehaviour {

    public int id;

    [SerializeField]
    bool conveyMode;
    bool conveyMove;
    float speed;

	// Use this for initialization
	void Awake () {

        conveyMode = false;
        LevelManager.enableActions += createActionScript;
        LevelManager.enableIngredients += createIngredientScript;
	}

    void OnDestroy()
    {
        LevelManager.enableActions -= createActionScript;
        LevelManager.enableIngredients -= createIngredientScript;
    }

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
        if (conveyMove) {
            this.transform.position = Vector3.MoveTowards(this.transform.position ,new Vector3(10f, 10f, 0f), Time.deltaTime * 10);
        }
	}

    void createActionScript()
    {
        if (!conveyMode) {
            if (gameObject.GetComponent<IngredientController>() != null)
            {
                Destroy(gameObject.GetComponent<IngredientController>());
            }
            if (id > 2 || id == -1)
            {
                gameObject.AddComponent<ShelfIngredientController>();
            }
        }
        else {
            gameObject.AddComponent<ConveyEvent>();
        }
    }

    void createIngredientScript()
    {
        if (!conveyMode)
        {
            if (id > 2)
            {
                if (gameObject.GetComponent<ShelfIngredientController>() != null)
                {
                    Destroy(gameObject.GetComponent<ShelfIngredientController>());
                }
            }
            gameObject.AddComponent<IngredientController>();
        }
        else {
            Destroy(gameObject.GetComponent<ConveyEvent>());
        }
    }

    public void conveyModeOn(int difficulty) {
        speed = difficulty;
        conveyMode = true;
        conveyMove = true;
        if (GetComponent<ShelfIngredientController>() != null) {
            Destroy(GetComponent<ShelfIngredientController>());
        }

        if (GetComponent<IngredientController>() != null) {
            Destroy(GetComponent<IngredientController>());
        }
    }
}

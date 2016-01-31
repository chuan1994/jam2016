using UnityEngine;
using System.Collections;
using System;

public class BaseIngredientController : MonoBehaviour {

    public int id;

    [SerializeField]
    bool conveyMode;
    bool conveyMove;

    [SerializeField]
    float speed;

    [SerializeField]
    Vector3 conveyTarget;

    public delegate void targetReached(GameObject go);
    public static event targetReached reached;

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
            this.transform.position = Vector3.MoveTowards(this.transform.position, conveyTarget, Time.deltaTime * speed);
        }

        if (this.transform.position == conveyTarget) {
            if (reached != null) {
                reached(this.gameObject);
            }
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
            if (id > 2 || id == -1)
            {
                if (gameObject.GetComponent<ShelfIngredientController>() != null)
                {
                    Destroy(gameObject.GetComponent<ShelfIngredientController>());
                }
            }
            if (id > -1)
            {
                gameObject.AddComponent<IngredientController>();
            }
        }
        else {
            Destroy(gameObject.GetComponent<ConveyEvent>());
        }
    }

    public void conveyModeOn(float speedSet)
    {
        speed = speedSet;
        conveyMode = true;
        conveyMove = true;
        if (GetComponent<ShelfIngredientController>() != null)
        {
            Destroy(GetComponent<ShelfIngredientController>());
        }

        if (GetComponent<IngredientController>() != null)
        {
            Destroy(GetComponent<IngredientController>());
        }
    }
}

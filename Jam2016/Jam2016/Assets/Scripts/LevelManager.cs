using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    GameObject mixIngredient1;
    GameObject mixIngredient2;

    public GameObject product;

    public delegate void disable();
    public static event disable disableScripts;

    public delegate void enableAction();
    public static event enableAction enableActions;

	// Use this for initialization
	void Start () {
        Subscribe();
	}

    void OnDestroy()
    {
        Unsubscribe();
    }

	// Update is called once per frame
	void Update () {
	
	}

    private void ProcessStash(Vector3 stashPosition)
    {
        product.transform.position = stashPosition;
        disableFunction();
    }

    //Upon click of an area for product action
    private void ProcessClick(GameObject clickedObject)
    {
        if (clickedObject.tag.Equals("trash"))
        {
            Destroy(product);
        }
        else if (clickedObject.tag.Equals("box"))
        {

        }
        disableFunction();
    }

    //Upon production from two ingredients
    private void ProcessProduct(GameObject product)
    {
        this.product = product;
        enableFunction();
    }

    private void enableFunction()
    {
        if (enableActions != null)
        {
            enableActions();
        }
    }

    private void disableFunction()
    {
        if (disableScripts != null)
        {
            disableScripts();
        }
    }


    //event subscription and unsubscription
    private void Subscribe()
    {
        ShelfIngredientController.ShelfMouseDown += ProcessStash;
        GeneralClickController.eventUponClick += ProcessClick;
    }

    private void Unsubscribe()
    {
        ShelfIngredientController.ShelfMouseDown -= ProcessStash;
        GeneralClickController.eventUponClick -= ProcessClick;
    }
}

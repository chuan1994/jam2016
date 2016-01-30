using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    GameObject mixIngredient1;
    GameObject mixIngredient2;

    public GameObject product;

    public delegate void enable();
    public static event enable enableActions;
    public static event enable enableIngredients;

    void Awake()
    {
        Subscribe();
    }

	// Use this for initialization
	void Start () {
        enableIngredient();
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
        enableIngredient();
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
        enableIngredient();
    }

    //Upon production from two ingredients
    private void ProcessProduct(GameObject product)
    {
        enableAction();
        this.product = product;
        Destroy(this.product.GetComponent<ShelfIngredientController>());
    }

    private void enableAction()
    {
        if (enableActions != null)
        {
            enableActions();
        }
    }

    private void enableIngredient()
    {
        if (enableIngredients != null)
        {
            enableIngredients();
        }
    }


    //event subscription and unsubscription
    private void Subscribe()
    {
        PentagonController.PentHandler += ProcessProduct;
        ShelfIngredientController.ShelfMouseDown += ProcessStash;
        GeneralClickController.eventUponClick += ProcessClick;
    }

    private void Unsubscribe()
    {
        ShelfIngredientController.ShelfMouseDown -= ProcessStash;
        GeneralClickController.eventUponClick -= ProcessClick;
        PentagonController.PentHandler -= ProcessProduct;
    }
}

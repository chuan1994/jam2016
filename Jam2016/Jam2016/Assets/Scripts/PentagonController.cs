using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PentagonController : MonoBehaviour {

    public delegate void PentagonHandler(GameObject g);
    public static event PentagonHandler PentHandler;

    [SerializeField]
    float Speed;

    [SerializeField]
    List<GameObject> IngreList;

    [SerializeField]
    List<GameObject> OutIngre;

    public GameObject Ingredient;
    Vector3 OriginalPos;

    [SerializeField]
    Vector3 IngredientPos;

    private bool move;

	// Use this for initialization
	void Start () {
        move = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (move)
        {
            if (Ingredient.transform.position != IngredientPos)
            {
                float step = Speed * Time.deltaTime;
                Ingredient.transform.position = Vector3.MoveTowards(Ingredient.transform.position, IngredientPos, step);
            }
            else
            {
                Instantiate(Ingredient, OriginalPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                move = false;
            }
        }

        if (IngreList.Count == 2)
        {
            if (!move)
            {
                JoinObject(OutIngre[0]);
            }
            
        }

    }

    void fireEvent(GameObject g)
    {
        if (PentHandler != null)
        {
            PentHandler(g);
        }
    }

    void JarHandler(GameObject g)
    {
        
        IngreList.Add(g);
        Ingredient = g;
        Debug.Log(Ingredient);
        OriginalPos = Ingredient.transform.position;

        if (IngreList.Count == 2)
        {
            IngredientPos = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        }
        else
        {
            IngredientPos = transform.position;
        }

        move = true;
        Destroy(Ingredient.GetComponent<IngredientController>());
    }

    void OnEnable()
    {
        IngredientController.JarHandler += JarHandler;
    }

    void OnDisable()
    {
        IngredientController.JarHandler -= JarHandler;
    }

    void CombinationCheck(List<GameObject> i)
    {
        if (i[0].name == "Eyeball")
        {
            if (i[1].name == "Eyeball") {
                if (PentHandler != null)
                {
                    PentHandler(OutIngre[0]);
                    Instantiate(OutIngre[0], new Vector3(10, -2, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                }
            }
            
        }
    }

    void JoinObject(GameObject newIngredient)
    {
        GameObject g;
        g = (GameObject)Instantiate(newIngredient, new Vector3(10, -2, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        for (int i = IngreList.Count - 1; i >= 0; i--)
        {
            Destroy(IngreList[i]);
        }
        IngreList = new List<GameObject>();
        fireEvent(g);
    }
}

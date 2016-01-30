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

    private bool move;

	// Use this for initialization
	void Start () {
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
                Instantiate(Ingredient, OriginalPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                move = false;
            }
        } 
        else
        {
            //Destroy(Ingredient);
        }

        if (IngreList.Count == 2)
        {
            GameObject g;
            if (!move)
            {
                g = (GameObject)Instantiate(OutIngre[0], new Vector3(10, -2, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                IngreList = new List<GameObject>();
                fireEvent(g);
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
}

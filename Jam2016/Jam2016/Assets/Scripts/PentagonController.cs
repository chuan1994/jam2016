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

    GameObject Ingredient;
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
            Destroy(Ingredient);
        }


        if (IngreList.Count == 2)
        {
            if (!move)
            {
                Instantiate(OutIngre[0], new Vector3(10, -2, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                for (int i = 0; i < 1; i++)
                {
                    IngreList.Remove(IngreList[i]);
                }
            }
            
        }

    }

    void JarHandler(GameObject g)
    {
        IngreList.Add(g);
        Ingredient = g;
        move = true;
        OriginalPos = Ingredient.transform.position;
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

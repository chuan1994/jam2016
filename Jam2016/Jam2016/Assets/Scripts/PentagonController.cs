using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PentagonController : MonoBehaviour
{

    [System.Serializable]
    public class TargetPos
    {

        public GameObject go;
        public Vector3 target;
        public bool move;
    }

    public delegate void PentagonHandler(GameObject g);
    public static event PentagonHandler PentHandler;


    public float Speed =10;


    public List<TargetPos> IngreList = new List<TargetPos>();

    public List<GameObject> OutIngre;

    public GameObject Ingredient;

    Vector3 IngredientPos;

    private bool move;

    // Use this for initialization
    void Start()
    {
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        int reachedIng = 0;
        foreach (TargetPos tp in new List<TargetPos>(IngreList))
        {
            float distance = Vector3.Distance(tp.go.transform.position, tp.target);
            if (distance > 0.01f)
            {
                float step = Speed * Time.deltaTime;
                tp.go.transform.position = Vector3.MoveTowards(tp.go.transform.position, tp.target, step);
            }
            else
            {
                tp.move = false;
                reachedIng++;
            }

            if (reachedIng == 2)
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

        if (IngreList.Count < 2)
        {
            GameObject clonedIngredient;

            if (g.GetComponent<BaseIngredientController>().id >= 0 && g.GetComponent<BaseIngredientController>().id < 3)
            {
                clonedIngredient = Instantiate(g, g.transform.position, g.transform.rotation)as GameObject;

            }
            else
            {

                clonedIngredient = g;
            }
            TargetPos tp = new TargetPos();
            tp.go = clonedIngredient;
            tp.go.transform.rotation = Quaternion.Euler(10f, 0f, 0f);
            tp.go.transform.localScale = new Vector3(0.3f, 0.3f, 0f);

            if (IngreList.Count == 0)
            {
                tp.target = new Vector3(-1.5f, 2.8f, -5f);
            }
            else
            {
                tp.target = new Vector3(1.5f, 2.8f, -5f);
            }

            tp.move = true;
            IngreList.Add(tp);
            //Destroy(Ingredient.GetComponent<IngredientController>());
        }
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
            if (i[1].name == "Eyeball")
            {
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
        Debug.Log(g);
        foreach (TargetPos tp in new List<TargetPos>(IngreList))
        {
            Destroy(tp.go);
        }

        IngreList.Clear();
        fireEvent(g);
    }

}

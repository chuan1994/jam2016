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


    public float Speed;


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

            if (tp.go.transform.position != tp.target)
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
    int index = 0;
    void JarHandler(GameObject g)
    {

        if (IngreList.Count < 2)
        {
            GameObject clonedIngredient;

            if (g.GetComponent<BaseIngredientController>().id > 0 && g.GetComponent<BaseIngredientController>().id < 3)
            {
                clonedIngredient = Instantiate(g);

            }
            else
            {
                clonedIngredient = g;
            }
            clonedIngredient = Instantiate(g);
            clonedIngredient.name += index++;
            TargetPos tp = new TargetPos();
            tp.go = clonedIngredient;

            if (IngreList.Count == 0)
            {
                tp.target = transform.position;

            }
            else
            {
                tp.target = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
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
        IngreList.Clear();
        fireEvent(g);
    }

}

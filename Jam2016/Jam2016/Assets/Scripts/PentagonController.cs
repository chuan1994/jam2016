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

    int reachedCount;
    int OutIndex;

    public List<TargetPos> IngreList = new List<TargetPos>();
    List<GameObject> SendObjects = new List<GameObject>();
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
            }


            if (IngreList.Count == 2 && IngreList[1].move == false)
            {
                for (int i = 0; i < IngreList.Count; i++)
                {
                    if (IngreList[i].go.transform.position.y < 4f)
                    {
                        Vector3 current = IngreList[i].go.transform.position;
                        Vector3 target = new Vector3(current.x, 5, current.z);
                        IngreList[i].go.transform.position = Vector3.MoveTowards(current, target, 10 * Time.deltaTime);
                        float currentTransparency = IngreList[i].go.GetComponent<SpriteRenderer>().color.a;
                        IngreList[i].go.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, currentTransparency - 0.1f);
                    }
                    else
                    {
                        reachedCount++;
                    }
                }

                if (reachedCount == 2) {
                    int OutputID = CombinationCheck(IngreList);
                    if (OutputID != 404)
                    {
                        for (int i = 0; i < OutIngre.Count; i++)
                        {
                            if (OutputID == OutIngre[i].GetComponent<BaseIngredientController>().id)
                                OutIndex = i;
                        }
                        JoinObject(OutIngre[OutIndex]);
                    }
                    SendObjects.Clear();
                    reachedCount = 0;
                }
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
            //Destroying so once selected as ingredient, cannot be clicked
            Destroy(clonedIngredient.GetComponent<IngredientController>());
            //----------- Andy
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

    private int CombinationCheck(List<TargetPos> t)
    {
        for (int i = 0; i < t.Count; i++)
        {
            SendObjects.Add(t[i].go);
        }

        int result = CombinationLibrary.GetCombinationResult(SendObjects);
        return result;
    }

    void JoinObject(GameObject newIngredient)
    {
        GameObject g;
        g = (GameObject)Instantiate(newIngredient, new Vector3(0f, 4.5f, -5f), Quaternion.Euler(new Vector3(10f, 0f, 0f)));
        g.transform.localScale = new Vector3(0.3f, 0.3f, 0f);
        foreach (TargetPos tp in new List<TargetPos>(IngreList))
        {
            Destroy(tp.go);
        }

        IngreList.Clear();
        fireEvent(g);
    }

}

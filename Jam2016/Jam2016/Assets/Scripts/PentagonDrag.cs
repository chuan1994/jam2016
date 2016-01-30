using UnityEngine;
using System.Collections;

public class PentagonDrag : MonoBehaviour {

    [SerializeField]
    Vector3 IngredientPos;

    bool move;

    [SerializeField]
    float Speed;

	// Use this for initialization
	void Start () {
        move = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (move)
        {
                if (transform.position != IngredientPos)
                {
                    float step = Speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, IngredientPos, step);
                }
                else
                {
                    move = false;
                }
        }
	}

    public void GetPos(Vector3 pos)
    {
        IngredientPos = pos;
    }

    public void EnableMove()
    {
        move = true;
    }
}

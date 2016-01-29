using UnityEngine;
using System.Collections;

public class IngredientController : MonoBehaviour {

    public delegate void IngredientMouseHandler(GameObject g);
    public event IngredientMouseHandler JarHandler;

    [SerializeField]
    float Speed;

    [SerializeField]
    private bool move;

	// Use this for initialization
	void Start () {
        move = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        /*if (move) {
            Vector3 target = new Vector3(0, 0, 0);
            if (transform.position != target) {
                float step = Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, step);
            }
            else {
                move = false;
            }
        }*/
    }

    void OnMouseDown()
    {
        if (JarHandler != null)
        {
            JarHandler(this.gameObject);
        }

        Destroy(this.gameObject);
    }
}

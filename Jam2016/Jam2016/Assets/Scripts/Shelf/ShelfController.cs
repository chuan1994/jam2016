
using UnityEngine;
using System.Collections;

public class ShelfController : MonoBehaviour {

    [SerializeField]
    GameObject crate;


    GameObject ingredent_0;
    GameObject ingredent_1;
   // GameObject ingredent_2;


    Vector3 pos0 = new Vector3(3.86f, 5.85f, -4.55f);
    Vector3 pos1 = new Vector3(7.16f, 5.85f, -4.55f);



    // Use this for initialization
    void Start () {
        ingredent_0 = Instantiate(crate);
        ingredent_0.transform.position = pos0;
        
        ingredent_1 = Instantiate(crate);
        ingredent_1.transform.position = pos1;
        
       // ingredent_2 = Instantiate(crate);
       // ingredent_2.transform.position = pos2;
    }
	
	// Update is called once per frame
	void Update () {


	
	}


    
}

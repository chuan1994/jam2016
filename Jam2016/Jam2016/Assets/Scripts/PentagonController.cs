using UnityEngine;
using System.Collections;

public class PentagonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        IngredientController.JarHandler += JarHandler;
    }
}

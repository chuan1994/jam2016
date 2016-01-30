using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    bool moving = false;

    [SerializeField]
    float speed;

    [SerializeField]
    Vector3 GamePlayCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            if (transform.position != GamePlayCamera)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, GamePlayCamera, step);
            }
            else
            {
                moving = false;
            }
        }
	}

    public void GameStart() {
        moving = true;
    }
}

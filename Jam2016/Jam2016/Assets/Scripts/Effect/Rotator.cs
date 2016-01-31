using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float speed = 200;
    void Update() {
		transform.localRotation = Quaternion.Euler(0, 0, -Time.time * speed);
    }
}
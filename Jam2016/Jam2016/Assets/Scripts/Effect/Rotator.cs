using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float speed = 0.1f;
    void Update() {
        transform.localRotation = Quaternion.Euler(0, -Time.time * speed, 0);
    }
}
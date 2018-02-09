using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    public float standingThreshold = 5f;
    public float distToRaise = 40f;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

    }

    public bool IsStanding() {
        Vector3 rotation = transform.rotation.eulerAngles;

        float tiltInX = Mathf.Abs(270 - rotation.x);
        float tiltInZ = Mathf.Abs(rotation.z);

        if (tiltInX < standingThreshold && tiltInZ < standingThreshold) {
            return true;
        }
        return false;
    }

    public void RaiseIfStanding() {
        if (IsStanding()) {
            rigidBody.useGravity = false;
            transform.Translate(new Vector3(0, distToRaise, 0), Space.World);
        }
    }

    public void Lower() {
        rigidBody.useGravity = true;
        transform.Translate(new Vector3(0, -distToRaise, 0), Space.World);
    }
}

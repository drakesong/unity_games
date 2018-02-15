using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour {
    public float timeSinceLastTrigger = 0f;

    private bool foundClearArea = false;

    void Update() {
        timeSinceLastTrigger += Time.deltaTime;

        if (timeSinceLastTrigger > 1f && Time.realtimeSinceStartup > 10f && !foundClearArea) {
            SendMessageUpwards("OnFindClearArea");
            foundClearArea = true;
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag != "Player") {
            timeSinceLastTrigger = 0f;
        }
    }
}

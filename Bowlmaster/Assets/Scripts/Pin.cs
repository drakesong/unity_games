﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    public float standingThreshold = 3f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }

    public bool IsStanding()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        float tiltInX = Mathf.Abs(270 - rotation.x);
        float tiltInZ = Mathf.Abs(rotation.z);

        if (tiltInX < standingThreshold && tiltInZ < standingThreshold)
        {
            return true;
        }
        return false;
    }
}

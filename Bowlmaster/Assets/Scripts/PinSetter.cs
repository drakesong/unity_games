﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public GameObject pinSet;
    public float distToRaise = 40f;
    public bool ballEnteredBox = false;

    private int lastStandingCount = -1;
    private float lastChangeTime;
    private Ball ball;
    private int lastSettledCount = 10;
    private Animator animator;
    private ActionMaster actionMaster = new ActionMaster();

    // Use this for initialization
    void Start() {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        standingDisplay.text = CountStanding().ToString();

        if (ballEnteredBox) {
            CheckStanding();
            standingDisplay.color = Color.red;
        }
    }

    int CountStanding() {
        int standing = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                standing++;
            }
        }
        return standing;
    }

    void CheckStanding() {
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount) {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f;
        if ((Time.time - lastChangeTime) > settleTime) {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled() {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        ActionMaster.Action action = actionMaster.Bowl(pinFall);

        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMaster.Action.EndTurn) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (action == ActionMaster.Action.Reset) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("End Game");
        }

        ball.Reset();
        lastStandingCount = -1;
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
    }

    public void RaisePins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.RaiseIfStanding();
            pin.transform.rotation = Quaternion.Euler(270f, 0, 0);
        }
    }

    public void LowerPins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.Lower();
        }
    }

    public void RenewPins() {
        Instantiate(pinSet, new Vector3(0, distToRaise, 1829), Quaternion.identity);
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}

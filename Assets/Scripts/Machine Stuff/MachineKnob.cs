using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineKnob : MonoBehaviour
{
    enum State { Idle, Dispensing, Displaying };
    State state;
    GameObject currentCapsule;

    public GameObject capsulePrefab;

    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;

            case State.Dispensing:
                if (Input.GetMouseButtonDown(0))
                {
                    SkipOpeningAnimation();
                }
                break;

            case State.Displaying:
                break;
        }
    }

    void OnMouseOver()
    {
        if (state == State.Idle && Input.GetMouseButtonDown(0))
        {
            state = State.Dispensing;
            LeanTween.rotateZ(gameObject, -720, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                currentCapsule = Instantiate(capsulePrefab);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            });
        }
    }

    void SkipOpeningAnimation()
    {

    }
}
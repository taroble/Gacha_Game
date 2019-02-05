using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineKnob : MonoBehaviour
{
    public enum State { Idle, Twisting, Dispensing, Displaying };
    State state;
    GameObject currentCapsule;

    public Image fadeScreenImage;

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

            case State.Twisting:
                break;

            case State.Dispensing:
                if (Input.GetMouseButtonDown(0))
                {
                    currentCapsule.GetComponent<CapsuleLTAnimation>().SkipAnimation();
                }
                break;

            case State.Displaying:
                if (Input.GetMouseButtonDown(0))
                {
                    currentCapsule.GetComponent<CapsuleLTAnimation>().RemoveSelf();
                    LeanTween.value(0.5f, 0, 0.25f).setOnUpdate((float value) =>
                    {
                        fadeScreenImage.color = new Color(fadeScreenImage.color.r, fadeScreenImage.color.g, fadeScreenImage.color.b, value);
                    });
                    state = State.Idle;
                }
                break;
        }
    }

    void OnMouseOver()
    {
        if (state == State.Idle && Input.GetMouseButtonDown(0))
        {
            state = State.Twisting;
            float rotateAmt = (Random.Range(0, 20) == 0) ? -6969f : -720f;
            LeanTween.rotateZ(gameObject, rotateAmt, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                state = State.Dispensing;
                currentCapsule = Instantiate(capsulePrefab);
                currentCapsule.GetComponent<CapsuleLTAnimation>().machineKnob = this;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            });
        }
    }

    public void SetState(State newState)
    {
        state = newState;
    }
}
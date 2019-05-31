using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MachineKnob : MonoBehaviour
{
    public enum State { Idle, Jammed, Twisting, Dispensing, Displaying };
    State state;
    GameObject currentCapsule;

    public Image fadeScreenImage;
    public GameObject capsulePrefab;
    private AudioSource aSource;
    public AudioClip[] sounds;


    void Start()
    {
        state = State.Idle;
        GameMaster.instance.UpdateCoinCounter();
        aSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;

            case State.Jammed:
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
            if (GameMaster.instance.GetCoinAmount() > 0)
            {
                GameMaster.instance.SubtractCoins(1);
                state = State.Twisting;
                float rotateAmt = (Random.Range(0, 20) == 0) ? -6969f : -720f;
                LeanTween.rotateZ(gameObject, rotateAmt, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
                {
                    state = State.Dispensing;
                    currentCapsule = Instantiate(capsulePrefab);
                    currentCapsule.GetComponent<CapsuleLTAnimation>().machineKnob = this;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                });
                playsound(0);

            }
            else
            {
                state = State.Jammed;
                StartCoroutine(JammedAnimation());
                playsound(1);
            }
        }
    }

    public void playsound(int clip)
    {
        aSource.clip = sounds[clip];
        aSource.Play();
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    IEnumerator JammedAnimation()
    {
        LeanTween.rotateZ(gameObject, 15, 0.15f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.15f);

        LeanTween.rotateZ(gameObject, -15, 0.15f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.15f);

        LeanTween.rotateZ(gameObject, 15, 0.15f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.15f);

        LeanTween.rotateZ(gameObject, -15, 0.15f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.15f);

        LeanTween.rotateZ(gameObject, 0, 0.15f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.15f);

        state = State.Idle;
    }
}
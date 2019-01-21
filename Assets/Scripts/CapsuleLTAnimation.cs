using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleLTAnimation : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite halfCapSprite;
    

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Enlarge();
    }

    void Update()
    {
        
    }



    void Enlarge()
    {
        StartCoroutine(GachaFullAnimation());
    }

    IEnumerator GachaFullAnimation()
    {
        //Float upwards, grow larger
        sr.sortingLayerName = "Foreground";
        sr.sortingOrder = 1;
        LeanTween.moveY(gameObject, transform.position.y + 4, 0.75f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(gameObject, transform.localScale * 2f, 0.75f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(0.65f);

        //Jut downwards
        LeanTween.moveY(gameObject, transform.position.y - 3f, 0.35f).setEase(LeanTweenType.easeInCubic);
        yield return new WaitForSeconds(0.35f);

        //Split open, revealing contents
        sr.enabled = false;

        GameObject lHalf = new GameObject("Left Half");
        lHalf.AddComponent<SpriteRenderer>().sprite = halfCapSprite;
        lHalf.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        lHalf.GetComponent<SpriteRenderer>().sortingOrder = 1;

        GameObject rHalf = new GameObject("Right Half");
        rHalf.AddComponent<SpriteRenderer>().sprite = halfCapSprite;
        rHalf.GetComponent<SpriteRenderer>().flipX = true;
        rHalf.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        rHalf.GetComponent<SpriteRenderer>().sortingOrder = 1;

        lHalf.transform.position = transform.position + Vector3.left;
        lHalf.transform.localScale *= 0.75f;
        rHalf.transform.position = transform.position + Vector3.right;
        rHalf.transform.localScale *= 0.75f;

        LeanTween.moveX(lHalf, lHalf.transform.position.x - 2, 0.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveX(rHalf, rHalf.transform.position.x + 2, 0.5f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.5f);
    }
}
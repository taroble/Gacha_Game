using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CapsuleLTAnimation : MonoBehaviour
{
    SpriteRenderer sr;
    Image fadeScreenImage;
    public Sprite halfCapSprite;

    public GameObject topTextPrefab;
    public GameObject bottomTextPrefab;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        fadeScreenImage = GameObject.FindGameObjectWithTag("Fade Canvas").transform.Find("Screen Fade").GetComponent<Image>();    //absolutely disgusting
        Enlarge();
    }

    void Enlarge()
    {
        StartCoroutine(GachaFullAnimation());
    }

    IEnumerator GachaFullAnimation()
    {
        //Float upwards, grow larger, fade screen
        sr.sortingLayerName = "Foreground";
        sr.sortingOrder = 1;
        //LeanTween.alpha(GameObject.FindGameObjectWithTag("Render Canvas").transform.Find("Screen Fade").gameObject, 0.25f, 0.75f);
        LeanTween.value(0, 0.25f, 0.75f).setOnUpdate((float value) =>
        {
            fadeScreenImage.color = new Color(fadeScreenImage.color.r, fadeScreenImage.color.g, fadeScreenImage.color.b, value);
        });
        LeanTween.moveY(gameObject, transform.position.y + 4, 0.75f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(gameObject, transform.localScale * 2f, 0.75f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(0.65f);

        //Jut downwards
        LeanTween.moveY(gameObject, transform.position.y - 3f, 0.35f).setEase(LeanTweenType.easeInCubic);
        yield return new WaitForSeconds(0.35f);

        //Split open, revealing contents
        sr.enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeHandler>().ShakeCamera(0.15f, 0.35f);
        Item receivedItem = GameMaster.instance.GrabRandomItem();

        GameObject lHalf = new GameObject("Left Half");
        lHalf.AddComponent<SpriteRenderer>().sprite = halfCapSprite;
        lHalf.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        lHalf.GetComponent<SpriteRenderer>().sortingOrder = 2;

        GameObject rHalf = new GameObject("Right Half");
        rHalf.AddComponent<SpriteRenderer>().sprite = halfCapSprite;
        rHalf.GetComponent<SpriteRenderer>().flipX = true;
        rHalf.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        rHalf.GetComponent<SpriteRenderer>().sortingOrder = 2;

        lHalf.transform.position = transform.position + Vector3.left;
        lHalf.transform.localScale *= 0.75f;
        rHalf.transform.position = transform.position + Vector3.right;
        rHalf.transform.localScale *= 0.75f;

        LeanTween.moveX(lHalf, lHalf.transform.position.x - 2, 0.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveX(rHalf, rHalf.transform.position.x + 2, 0.5f).setEase(LeanTweenType.easeOutCubic);

        GameObject gachaItem = new GameObject("Gacha Item");
        gachaItem.AddComponent<SpriteRenderer>().sprite = receivedItem.image;
        gachaItem.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        gachaItem.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        gachaItem.GetComponent<SpriteRenderer>().sortingOrder = 2;
        LeanTween.scale(gachaItem, gachaItem.transform.localScale * 2, 1.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.alpha(gachaItem, 1, 0.25f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.5f);

        //Spawn text objects
        GameObject topText = Instantiate(topTextPrefab);
        topText.GetComponent<TextMeshProUGUI>().text = "You received:";
        topText.transform.SetParent(GameObject.FindGameObjectWithTag("Text Canvas").transform, false);
        yield return new WaitForSeconds(0.75f);

        GameObject bottomText = Instantiate(bottomTextPrefab);
        bottomText.transform.SetParent(GameObject.FindGameObjectWithTag("Text Canvas").transform, false);
        bottomText.GetComponent<TextMeshProUGUI>().text = receivedItem.itemName;
        yield return null;
    }
}
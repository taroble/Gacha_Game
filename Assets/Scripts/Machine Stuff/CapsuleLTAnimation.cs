using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CapsuleLTAnimation : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite commonCapsule;
    public Sprite uncommonCapsule;
    public Sprite rareCapsule;
    public Sprite ultraRareCapsule;
    public Sprite commonCapsuleTop;
    public Sprite uncommonCapsuleTop;
    public Sprite rareCapsuleTop;
    public Sprite ultraRareCapsuleTop;
    public Sprite capsuleBottom;

    [HideInInspector]
    public MachineKnob machineKnob;
    [HideInInspector]
    public Item receivedItem;

    public GameObject topTextPrefab;
    public GameObject bottomTextPrefab;

    GameObject lHalf;
    GameObject rHalf;
    GameObject gachaItem;
    GameObject topText;
    GameObject bottomText;



    void Start()
    {
        receivedItem = GameMaster.instance.GrabRandomItem();
        sr = GetComponent<SpriteRenderer>();
        switch (receivedItem.rarity)
        {
            case Item.Rarity.Common:
                sr.sprite = commonCapsule;
                break;
            case Item.Rarity.Uncommon:
                sr.sprite = uncommonCapsule;
                break;
            case Item.Rarity.Rare:
                sr.sprite = rareCapsule;
                break;
            case Item.Rarity.UltraRare:
                sr.sprite = ultraRareCapsule;
                break;
        }
        Enlarge();
    }

    void Enlarge()
    {
        StartCoroutine(GachaFullAnimation());
    }

    public void SkipAnimation()
    {
        LeanTween.cancel(gameObject);
        StopAllCoroutines();
        Destroy(lHalf);
        Destroy(rHalf);
        Destroy(gachaItem);
        Destroy(topText);
        Destroy(bottomText);
        SpawnAllObjectsInEndOfAnimationState();
        machineKnob.fadeScreenImage.color = new Color(machineKnob.fadeScreenImage.color.r, machineKnob.fadeScreenImage.color.g, machineKnob.fadeScreenImage.color.b, 0.5f);
        machineKnob.SetState(MachineKnob.State.Displaying);
    }

    public void RemoveSelf()
    {
        LeanTween.cancel(gameObject);
        Destroy(lHalf);
        Destroy(rHalf);
        Destroy(gachaItem);
        Destroy(topText);
        Destroy(bottomText);
        Destroy(gameObject);
    }



    IEnumerator GachaFullAnimation()
    {
        //Fall downwards
        LeanTween.moveY(gameObject, -3.8f, 0.5f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.5f);

        //Float upwards, grow larger, fade screen
        sr.sortingLayerName = "Foreground";
        sr.sortingOrder = 1;
        LeanTween.value(gameObject, 0, 0.5f, 1.5f).setOnUpdate((float value) =>
        {
            machineKnob.fadeScreenImage.color = new Color(machineKnob.fadeScreenImage.color.r, machineKnob.fadeScreenImage.color.g, machineKnob.fadeScreenImage.color.b, value);
        });
        LeanTween.moveY(gameObject, 3, 0.75f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(gameObject, transform.localScale * 2f, 0.75f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.rotateZ(gameObject, 810, 0.75f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.65f);

        //Jut downwards
        LeanTween.moveY(gameObject, 0, 0.35f).setEase(LeanTweenType.easeInCubic);
        yield return new WaitForSeconds(0.35f);

        //Split open, revealing contents
        sr.enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeHandler>().ShakeCamera(0.15f, 0.35f);

        lHalf = new GameObject("Left Half");
        lHalf.AddComponent<SpriteRenderer>();
        switch (receivedItem.rarity)
        {
            case Item.Rarity.Common:
                lHalf.GetComponent<SpriteRenderer>().sprite = commonCapsuleTop;
                break;
            case Item.Rarity.Uncommon:
                lHalf.GetComponent<SpriteRenderer>().sprite = uncommonCapsuleTop;
                break;
            case Item.Rarity.Rare:
                lHalf.GetComponent<SpriteRenderer>().sprite = rareCapsuleTop;
                break;
            case Item.Rarity.UltraRare:
                lHalf.GetComponent<SpriteRenderer>().sprite = ultraRareCapsuleTop;
                break;
        }
        lHalf.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        lHalf.GetComponent<SpriteRenderer>().sortingOrder = 2;

        rHalf = new GameObject("Right Half");
        rHalf.transform.eulerAngles = new Vector3(rHalf.transform.eulerAngles.x, rHalf.transform.eulerAngles.y, 90);
        rHalf.AddComponent<SpriteRenderer>().sprite = capsuleBottom;
        rHalf.GetComponent<SpriteRenderer>().flipX = true;
        rHalf.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        rHalf.GetComponent<SpriteRenderer>().sortingOrder = 2;

        lHalf.transform.position = transform.position + Vector3.left;
        lHalf.transform.localScale *= 2;
        lHalf.transform.eulerAngles = new Vector3(lHalf.transform.eulerAngles.x, lHalf.transform.eulerAngles.y, 90);
        rHalf.transform.position = transform.position + Vector3.right;
        rHalf.transform.localScale *= 2;
        rHalf.transform.eulerAngles = new Vector3(rHalf.transform.eulerAngles.x, rHalf.transform.eulerAngles.y, 90);

        LeanTween.moveX(lHalf, lHalf.transform.position.x - 3, 0.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveX(rHalf, rHalf.transform.position.x + 3, 0.5f).setEase(LeanTweenType.easeOutCubic);

        gachaItem = new GameObject("Gacha Item");
        gachaItem.AddComponent<SpriteRenderer>().sprite = receivedItem.image;
        gachaItem.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        gachaItem.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        gachaItem.GetComponent<SpriteRenderer>().sortingOrder = 3;
        LeanTween.scale(gachaItem, gachaItem.transform.localScale * 2, 1.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.alpha(gachaItem, 1, 0.25f).setEase(LeanTweenType.easeOutCubic);
        yield return new WaitForSeconds(0.5f);

        //Spawn text objects
        topText = Instantiate(topTextPrefab);
        topText.GetComponent<TextMeshProUGUI>().text = "You received:";
        topText.transform.SetParent(GameObject.FindGameObjectWithTag("Text Canvas").transform, false);
        TextMeshProUGUI topTMP = topText.GetComponent<TextMeshProUGUI>();
        topTMP.color = new Color(topTMP.color.r, topTMP.color.g, topTMP.color.b, 0);
        LeanTween.moveY(topText, transform.position.y + 3, 1.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(topText, new Vector2(3.5f, 3.5f), 1.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.value(gameObject, 0, 1, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnUpdate((float value) =>
        {
            topTMP.color = new Color(topTMP.color.r, topTMP.color.g, topTMP.color.b, value);
        });
        yield return new WaitForSeconds(0.75f);

        bottomText = Instantiate(bottomTextPrefab);
        bottomText.transform.SetParent(GameObject.FindGameObjectWithTag("Text Canvas").transform, false);
        bottomText.GetComponent<TextMeshProUGUI>().text = receivedItem.itemName;
        TextMeshProUGUI bottomTMP = bottomText.GetComponent<TextMeshProUGUI>();
        bottomTMP.color = new Color(bottomTMP.color.r, bottomTMP.color.g, bottomTMP.color.b, 0);
        LeanTween.moveY(bottomText, transform.position.y - 3, 1.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(bottomText, new Vector2(3.5f, 3.5f), 1.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.value(gameObject, 0, 1, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnUpdate((float value) =>
        {
            bottomTMP.color = new Color(bottomTMP.color.r, bottomTMP.color.g, bottomTMP.color.b, value);
        });
        yield return new WaitForSeconds(0.75f);

        machineKnob.SetState(MachineKnob.State.Displaying);
    }



    void SpawnAllObjectsInEndOfAnimationState()
    {
        sr.enabled = false;

        lHalf = new GameObject("Left Half");
        lHalf.transform.Translate(new Vector2(-4, 0));
        lHalf.transform.localScale *= 2;
        lHalf.transform.eulerAngles = new Vector3(lHalf.transform.eulerAngles.x, lHalf.transform.eulerAngles.y, 90);
        lHalf.AddComponent<SpriteRenderer>();
        switch (receivedItem.rarity)
        {
            case Item.Rarity.Common:
                lHalf.GetComponent<SpriteRenderer>().sprite = commonCapsuleTop;
                break;
            case Item.Rarity.Uncommon:
                lHalf.GetComponent<SpriteRenderer>().sprite = uncommonCapsuleTop;
                break;
            case Item.Rarity.Rare:
                lHalf.GetComponent<SpriteRenderer>().sprite = rareCapsuleTop;
                break;
            case Item.Rarity.UltraRare:
                lHalf.GetComponent<SpriteRenderer>().sprite = ultraRareCapsuleTop;
                break;
        }
        lHalf.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        lHalf.GetComponent<SpriteRenderer>().sortingOrder = 2;

        rHalf = new GameObject("Right Half");
        rHalf.transform.Translate(new Vector2(4, 0));
        rHalf.transform.localScale *= 2;
        rHalf.transform.eulerAngles = new Vector3(rHalf.transform.eulerAngles.x, rHalf.transform.eulerAngles.y, 90);
        rHalf.AddComponent<SpriteRenderer>().sprite = capsuleBottom;
        rHalf.GetComponent<SpriteRenderer>().flipX = true;
        rHalf.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        rHalf.GetComponent<SpriteRenderer>().sortingOrder = 2;

        gachaItem = new GameObject("Gacha Item");
        gachaItem.transform.localScale *= 2;
        gachaItem.AddComponent<SpriteRenderer>().sprite = receivedItem.image;
        gachaItem.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        gachaItem.GetComponent<SpriteRenderer>().sortingOrder = 3;

        topText = Instantiate(topTextPrefab);
        topText.GetComponent<TextMeshProUGUI>().text = "You received:";
        topText.transform.SetParent(GameObject.FindGameObjectWithTag("Text Canvas").transform, false);
        topText.transform.localScale = new Vector2(3.5f, 3.5f);
        topText.transform.Translate(Vector2.up * 3);

        bottomText = Instantiate(bottomTextPrefab);
        bottomText.transform.SetParent(GameObject.FindGameObjectWithTag("Text Canvas").transform, false);
        bottomText.GetComponent<TextMeshProUGUI>().text = receivedItem.itemName;
        bottomText.transform.localScale = new Vector2(3.5f, 3.5f);
        bottomText.transform.Translate(Vector2.down * 3);
    }
}
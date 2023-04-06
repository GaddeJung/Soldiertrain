using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponShop : MonoBehaviour, IDragHandler
{
    public GameObject weaponShop;
    public AudioClip clip;


    // 무기 UI 드래그
    private Vector2 dragStartPosition;
    private Vector2 contentStartPosition;

    [SerializeField] private RectTransform contentRectTransform;
    [SerializeField] private float sensitivity = 1f;

    private void Awake()
    {
        contentStartPosition = contentRectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        delta.y = 0;
        contentRectTransform.anchoredPosition += delta * sensitivity;
    }

    public void OpenWeaponShopButton()
    {
        weaponShop.SetActive(true);
        contentRectTransform.anchoredPosition = contentStartPosition;
        SFXSound.instance.SFXPlay("Button", clip);
    }

    public void EixtWeaponShopButton()
    {
        weaponShop.SetActive(false);
        SFXSound.instance.SFXPlay("Button", clip);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopBuy : MonoBehaviour
{
    public List<BigInteger> price;
    public int tapPlus;
    public bool[] buyClear;

    public Text[] priceText;
    public GameObject[] priceImage;
    public GameObject[] buyButton;

    private string[] moneyUnitArr = new string[] { "��", "��", "��", "��", "��", "��" };

    public GameObject gameManager;

    public void Awake()
    {
        price = new List<BigInteger>(14);
        buyClear = new bool [14];

        for (int i = 0; i < priceText.Length; i++)
        {
            if (i == 0)
            {
                price.Add(3000);
                priceText[i].text = getMoneyText(i);
            }
            else
            {
                price.Add(price[i - 1] * 5);
                priceText[i].text = getMoneyText(i);
            }
            
        }
    }

    private void Update()
    {
        for (int i = 0; i < buyButton.Length; i++)
        {
            if (gameManager.GetComponent<GameManager>().money < price[i])
            {
                buyButton[i].SetActive(false);
            }

            else if (gameManager.GetComponent<GameManager>().money >= price[i])
            {
                buyButton[i].SetActive(true);
            }
            if (buyClear[i])
            {
                priceImage[i].SetActive(false);
            }
        }


    }
    public void OnBuyButtonClick(int index)
    {
        // �ش� ��ư ��Ȱ��ȭ ó��
        priceImage[index].SetActive(false);

        // ���� ����
        gameManager.GetComponent<GameManager>().money -= price[index];

        // ���� �ؽ�Ʈ ������Ʈ
        buyClear[index] = true;
        priceText[index].text = getMoneyText(index);
        tapPlus += (index + 1) * 2;
    }

    private string getMoneyText(int index)
    {
        int placN = 4;
        BigInteger value = price[index];
        List<int> numList = new List<int>();
        int p = (int)Mathf.Pow(10, placN);

        do
        {
            numList.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);

        string retStr = "";

        for (int i = 0; i < numList.Count; i++)
        {
            retStr = numList[i] + moneyUnitArr[i] + retStr;
        }
        return retStr;
    }
}
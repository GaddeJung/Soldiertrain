using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;


public class SoldierShopBuy : MonoBehaviour
{
    public List<BigInteger> price; // ���۽� �����ϴ� ���
    public List<BigInteger> upgradePrice; // ������ �� ���
    public List<BigInteger> secondMoney; // ���� �ʴ� ������ ��
    public List<int> lv;
    public BigInteger secondMoneyFull; // ����Ʈ�ӴϿ��� ����Ʈ ���� ���� �Ű� �ֱ� ���� ��

    public Text[] priceText;
    public Text[] exText;
    public Text[] lvText;
    public GameObject[] priceImage;
    public GameObject[] buyButton;
    public GameObject[] soldier;
    public GameObject soldierShop;

    private string[] moneyUnitArr = new string[] { "��", "��", "��", "��", "��", "��" };

    public GameObject gameManager;

    public AudioClip clip;

    public void Awake()
    {
        price = new List<BigInteger>(4);
        upgradePrice = new List<BigInteger>(4);
        secondMoney = new List<BigInteger>(4);
        for (int i = 0; i < priceText.Length; i++)
        {
            
                if (i == 0)
                {
                    price.Add(10000);
                    priceText[i].text = getMoneyText(i);
                }
                else if (i > 0)
                {
                    price.Add(price[i - 1] * 10);
                    priceText[i].text = getMoneyText(i);
                }

        }

        for (int i = 0; i < priceText.Length; i++)
        {
            if (i == 0)
            {
                upgradePrice.Add(price[i]+(price[i]/10));
            }
            else if (i > 0)  
            {
                upgradePrice.Add(upgradePrice[i - 1] * 10);
            }
        }


    }

    private void Update()
    {
        for (int i = 0; i < priceText.Length; i++)
        {
            if (gameManager.GetComponent<GameManager>().money < price[i])
            {
                buyButton[i].SetActive(false);
            }

            else if (gameManager.GetComponent<GameManager>().money >= price[i])
            {
                buyButton[i].SetActive(true);
            }
        }

        for (int i = 0; i < priceText.Length; i++)
        {
            if (lv[i] != 0)
            {
                soldier[i].SetActive(true);
                priceText[i].text = getUpgradMoneyText(i);
                lvText[i].text = "Lv." + lv[i];
                exText[i].text = "�ʴ� " + secondMoney[i] * lv[i] + "��";

            }
        }

    }
    public void OnBuyButtonClick(int index)
    {
        if (lv[index] == 0)
        {
            // ���� ����
            gameManager.GetComponent<GameManager>().money -= price[index];

            // �ʴ� ���ʽ� ��
            if (secondMoney.Count <= index)
            {
                secondMoney.Add(price[index] / 100);
            }
            else
            {
                secondMoney[index] = price[index] / 100;
            }

            secondMoneyFull += secondMoney[index];

            soldier[index].SetActive(true);

            // ������
            lv[index] ++;

            // ���� �ؽ�Ʈ ������Ʈ �� ��������
            priceText[index].text = getUpgradMoneyText(index);
            lvText[index].text = "Lv." + lv[index];
            exText[index].text = "�ʴ� " + secondMoney[index] * lv[index] + "��";

        }

        else if (lv[index] > 0)
        {
            // ���� ����
            gameManager.GetComponent<GameManager>().money -= upgradePrice[index];


            // �ʴ� ���ʽ� ��
            secondMoney.Add(secondMoney[index] * lv[index]);

            // ������
            lv[index]++;

            secondMoneyFull += secondMoney[index];

            // ���� �ؽ�Ʈ ������Ʈ �� ��������
            upgradePrice[index] = (upgradePrice[index] + (upgradePrice[index] / 10));
            lvText[index].text = "Lv." + lv[index];
            priceText[index].text = getUpgradMoneyText(index);
            exText[index].text = "���� �ʴ� " + secondMoney[index] * lv[index] + "��";
        }
    }
    private string getUpgradMoneyText(int index)
    {
        int placN = 4;
        BigInteger value = upgradePrice[index];
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
    public void OpenSoldierShopButton()
    {
        soldierShop.SetActive(true);
        SFXSound.instance.SFXPlay("Button", clip);
    }

    public void EixtSoldierShopButton()
    {
        soldierShop.SetActive(false);
        SFXSound.instance.SFXPlay("Button", clip);
    }

}

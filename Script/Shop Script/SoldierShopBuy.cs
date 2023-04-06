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
    public List<BigInteger> price; // 시작시 구매하는 비용
    public List<BigInteger> upgradePrice; // 레벨업 당 비용
    public List<BigInteger> secondMoney; // 각각 초당 들어오는 돈
    public List<int> lv;
    public BigInteger secondMoneyFull; // 세컨트머니에서 리스트 총합 돈을 옮겨 넣기 위한 곳

    public Text[] priceText;
    public Text[] exText;
    public Text[] lvText;
    public GameObject[] priceImage;
    public GameObject[] buyButton;
    public GameObject[] soldier;
    public GameObject soldierShop;

    private string[] moneyUnitArr = new string[] { "원", "만", "억", "조", "경", "해" };

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
                exText[i].text = "초당 " + secondMoney[i] * lv[i] + "원";

            }
        }

    }
    public void OnBuyButtonClick(int index)
    {
        if (lv[index] == 0)
        {
            // 가격 차감
            gameManager.GetComponent<GameManager>().money -= price[index];

            // 초당 보너스 돈
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

            // 레벨업
            lv[index] ++;

            // 가격 텍스트 업데이트 및 가격조정
            priceText[index].text = getUpgradMoneyText(index);
            lvText[index].text = "Lv." + lv[index];
            exText[index].text = "초당 " + secondMoney[index] * lv[index] + "원";

        }

        else if (lv[index] > 0)
        {
            // 가격 차감
            gameManager.GetComponent<GameManager>().money -= upgradePrice[index];


            // 초당 보너스 돈
            secondMoney.Add(secondMoney[index] * lv[index]);

            // 레벨업
            lv[index]++;

            secondMoneyFull += secondMoney[index];

            // 가격 텍스트 업데이트 및 가격조정
            upgradePrice[index] = (upgradePrice[index] + (upgradePrice[index] / 10));
            lvText[index].text = "Lv." + lv[index];
            priceText[index].text = getUpgradMoneyText(index);
            exText[index].text = "현재 초당 " + secondMoney[index] * lv[index] + "원";
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

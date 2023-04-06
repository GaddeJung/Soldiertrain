using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip touchMoneyClip;
    public AudioClip UpgradeClip;

    public int level = 0;
    public int levelCount = 0;

    public float upgradebuy = 4;
    public BigInteger upgradebuy2;
    public BigInteger upgradesell = 100;

    // 닉네임
    public string playerName;
    // 현재 돈
    public BigInteger money;
    // 시작 업그레이드 돈
    public BigInteger startMoney;
    // 터치시 총합 들어오는 돈
    public BigInteger touchMoney;
    // 시간마다 들어오는 돈
    public BigInteger secondMoney;

    // 상정 오브젝트
    public GameObject weaponbuy;
    public GameObject soldierbuy;

    // 게임종료 UI
    public GameObject ExitUI;

    void Start()
    {
        Invoke("SecondMoney",1);
        startMoney = level;
    }

    void Update()
    {
        touchMoney = ((startMoney+100000) +(startMoney * weaponbuy.GetComponent<WeaponShopBuy>().tapPlus));
        // 초당 합계 계산
        secondMoney = (startMoney + soldierbuy.GetComponent<SoldierShopBuy>().secondMoneyFull);
        // 터치
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            money += touchMoney;
            SFXSound.instance.SFXPlay("touchmoney", touchMoneyClip);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitUI.SetActive(true);
        }

    }

    public void UpgradeButton()
    {
        if (level == 100)
        {
            upgradebuy2 = (int)upgradebuy;
        }
        if (money >= upgradesell)
        {
            if (level == 0)
            {
                level++;
                levelCount++;
                upgradesell += (int)upgradebuy;
                startMoney = level;
            }

            else if (level >= 100)
            {
                level++;
                upgradebuy2 += (upgradebuy2 / 30);
                upgradesell += upgradebuy2;
                startMoney = level;
                levelCount++;
                if (levelCount == 100)
                {
                    upgradebuy = (upgradebuy * 2f);
                    levelCount = 1;
                }

            }

            else
            {
                level++;
                upgradebuy = (upgradebuy * 1.1f);
                upgradesell += (int)upgradebuy;
                startMoney = level;
                levelCount++;               
            }

            money -= upgradesell;
            SFXSound.instance.SFXPlay("upgradebuy", UpgradeClip);
        }
    }

    void SecondMoney()
    {
        money += secondMoney;
        Invoke("SecondMoney", 1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void NotExitButton()
    {
        ExitUI.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text money;
    public Text upgradeSell;
    public Text notUpgradeSell;
    public Text level;
    public Text tapMoney;
    public Text secondMoney;

    public GameObject gameManager;
    public GameObject upgradeButton;

    private string[] moneyUnitArr = new string[] {"원", "만","억", "조","경", "해"};
    private string getMoneyText()
    {
        int placN = 4;
        BigInteger value = gameManager.GetComponent<GameManager>().money;
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
    private string gettapMoneyText()
    {
        int placN = 4;
        BigInteger value = gameManager.GetComponent<GameManager>().touchMoney;
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
    private string getSecondMoneyText()
    {
        int placN = 4;
        BigInteger value = gameManager.GetComponent<GameManager>().secondMoney;
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
    private string getUpgradeSellText()
    {
        int placN = 4;
        BigInteger value = gameManager.GetComponent<GameManager>().upgradesell;
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



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        money.text = getMoneyText();
        upgradeSell.text = getUpgradeSellText();
        notUpgradeSell.text = getUpgradeSellText();
        level.text = "Level : " + gameManager.GetComponent<GameManager>().level;
        tapMoney.text = "탭당 " + gettapMoneyText();
        secondMoney.text = "초당 " + getSecondMoneyText();

        // 업그레이드 버튼 활성화
        if (gameManager.GetComponent<GameManager>().money >= gameManager.GetComponent<GameManager>().upgradesell)
        {
            upgradeButton.SetActive(true);
        }
        else
            upgradeButton.SetActive(false);

    }
}

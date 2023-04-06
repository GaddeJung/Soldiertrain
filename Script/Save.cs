using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;

// ������ �����͸� Ŭ������ ����
[System.Serializable]
public class PlayerData
{
    // GameManager Script ���̺� �����
    // �÷��̾� �̸�
    public string playerName;
    // �÷��̾� ��
    public BigInteger money;
    // �÷��̾� ����
    public int level;
    public float upgradebuy;
    public BigInteger upgradebuy2;
    public BigInteger upgradesell;

    // Soldier Script ���̺� ����
    public List<BigInteger> price; // ���۽� �����ϴ� ���
    public List<BigInteger> upgradePrice; // ������ �� ���
    public List<BigInteger> secondMoney; // ���� �ʴ� ������ ��
    public List<int> lv;
    public BigInteger secondMoneyFull; // ����Ʈ�ӴϿ��� ����Ʈ ���� ���� �Ű� �ֱ� ���� ��

    // Weapon Script ���̺� ����
    public int tapPlus;
    public bool[] buyClear;

}

public class Save : MonoBehaviour
{

    private string dataFilePath;

    public GameManager gameManager;
    public SoldierShopBuy solderManager;
    public WeaponShopBuy WeaponManager;
    public PlayerData data;

    private float timer = 5f; // Ÿ�̸� ���� ���� �� 5�ʷ� �ʱ�ȭ
    private void Awake()
    {
        data = new PlayerData();
        data.buyClear = new bool[14];
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        solderManager = FindObjectOfType<SoldierShopBuy>();
        WeaponManager = FindObjectOfType<WeaponShopBuy>();
        // ������ ���� ��� ����
        dataFilePath = Application.persistentDataPath + "/playerData.dat";

        // ����� ������ �ҷ�����
        data = LoadData();

        // ���ο� ������ ���� �� ����
        if (data != null)
        {
            gameManager.playerName = data.playerName;
            gameManager.money = data.money;
            gameManager.level = data.level;

            solderManager.price = data.price;
            solderManager.upgradePrice = data.upgradePrice;
            solderManager.secondMoney = data.secondMoney;
            solderManager.lv = data.lv;

            WeaponManager.tapPlus = data.tapPlus;
            WeaponManager.buyClear = data.buyClear;

        }

        Debug.Log(data.playerName);
        Debug.Log(data.money);
        Debug.Log(data.level);
        Debug.Log(data.price);
        Debug.Log(data.upgradePrice);
        Debug.Log(data.secondMoney);
        Debug.Log(data.tapPlus);
        Debug.Log(data.lv);
        Debug.Log(data.buyClear);

    }

    private void Update()
    {
        // Ÿ�̸Ӱ� 0 ������ ������ ������ ����
        if (timer <= 0f)
        {
            // ������ ����
            PlayerData data = new PlayerData();
            data.playerName = gameManager.playerName;
            data.money = gameManager.money;
            data.level = gameManager.level;

            data.price = solderManager.price;
            data.upgradePrice = solderManager.upgradePrice;
            data.secondMoney = solderManager.secondMoney;
            data.lv = solderManager.lv;

            data.tapPlus = WeaponManager.tapPlus;
            data.buyClear = WeaponManager.buyClear;

            SaveData(data);

            // Ÿ�̸� �缳��
            timer = 5f;
            Debug.Log(data.playerName);
            Debug.Log(data.money);
            Debug.Log(data.level);
            Debug.Log(data.price);
            Debug.Log(data.upgradePrice);
            Debug.Log(data.secondMoney);
            Debug.Log(data.tapPlus);
            Debug.Log(data.lv);
            Debug.Log(data.buyClear);

        }

        // �� �����Ӹ��� Ÿ�̸� ����
        timer -= Time.deltaTime;
    }

    // ������ ���� �Լ�
    private void SaveData(PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataFilePath);
        // �����͸� Serialize �Ͽ� ����Ʈ �迭�� ��ȯ �� ���Ͽ� ����
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, data);

        byte[] bytes = ms.ToArray();
        file.Write(bytes, 0, bytes.Length);
        file.Close();

        Debug.Log("Data saved to: " + dataFilePath);

    }

    // ������ �ε� �Լ�
    private PlayerData LoadData()
    {
        PlayerData data;

        // ����� ������ ������ ������ �ҷ�����
        if (File.Exists(dataFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataFilePath, FileMode.Open);
            // ���Ͽ��� �����͸� �о�� Deserialize �Ͽ� ��ü�� ��ȯ
            byte[] bytes = new byte[file.Length];
            file.Read(bytes, 0, bytes.Length);
            file.Close();

            MemoryStream ms = new MemoryStream(bytes);
            data = (PlayerData)bf.Deserialize(ms);
        }
        // ����� ������ ������ ���ο� ������ ����
        else
        {
            data = null;
        }

        return data;
    }
}
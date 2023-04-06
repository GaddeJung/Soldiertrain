using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;

// 저장할 데이터를 클래스로 정의
[System.Serializable]
public class PlayerData
{
    // GameManager Script 세이브 내용들
    // 플레이어 이름
    public string playerName;
    // 플레이어 돈
    public BigInteger money;
    // 플레이어 레벨
    public int level;
    public float upgradebuy;
    public BigInteger upgradebuy2;
    public BigInteger upgradesell;

    // Soldier Script 세이브 내용
    public List<BigInteger> price; // 시작시 구매하는 비용
    public List<BigInteger> upgradePrice; // 레벨업 당 비용
    public List<BigInteger> secondMoney; // 각각 초당 들어오는 돈
    public List<int> lv;
    public BigInteger secondMoneyFull; // 세컨트머니에서 리스트 총합 돈을 옮겨 넣기 위한 곳

    // Weapon Script 세이브 내용
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

    private float timer = 5f; // 타이머 변수 선언 및 5초로 초기화
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
        // 데이터 파일 경로 설정
        dataFilePath = Application.persistentDataPath + "/playerData.dat";

        // 저장된 데이터 불러오기
        data = LoadData();

        // 새로운 데이터 생성 및 저장
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
        // 타이머가 0 이하일 때마다 데이터 저장
        if (timer <= 0f)
        {
            // 데이터 저장
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

            // 타이머 재설정
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

        // 매 프레임마다 타이머 감소
        timer -= Time.deltaTime;
    }

    // 데이터 저장 함수
    private void SaveData(PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataFilePath);
        // 데이터를 Serialize 하여 바이트 배열로 변환 후 파일에 쓰기
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, data);

        byte[] bytes = ms.ToArray();
        file.Write(bytes, 0, bytes.Length);
        file.Close();

        Debug.Log("Data saved to: " + dataFilePath);

    }

    // 데이터 로드 함수
    private PlayerData LoadData()
    {
        PlayerData data;

        // 저장된 파일이 있으면 데이터 불러오기
        if (File.Exists(dataFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataFilePath, FileMode.Open);
            // 파일에서 데이터를 읽어와 Deserialize 하여 객체로 변환
            byte[] bytes = new byte[file.Length];
            file.Read(bytes, 0, bytes.Length);
            file.Close();

            MemoryStream ms = new MemoryStream(bytes);
            data = (PlayerData)bf.Deserialize(ms);
        }
        // 저장된 파일이 없으면 새로운 데이터 생성
        else
        {
            data = null;
        }

        return data;
    }
}
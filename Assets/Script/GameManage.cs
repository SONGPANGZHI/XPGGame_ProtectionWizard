using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;

public class GameManage : Singleton<GameManage>
{
    //public static GameManage Instance { get; private set; }
    public enum GameStateType
    {
        GameStart,
        TheFirstPass,
        TheSecondPass,
        GameEnd
    }

    public GameStateType gameStateType;
    public GameObject player;
    public GameObject enemy;
    public GameObject enemySecond;
    public PlayerManager playerManager;
    public int getTotalScore;//得分界面显示的得分

    // 引用你的cube预制体
    public GameObject[] cubePrefabs;

    // 生成区域的大小
    public float spawnArea = 5f;

    // 开始随机生成协程的标志
    private bool shouldSpawn = false;

    //生成间隔时间范围
    public float minGenerateTime = 10f;
    public float maxGenerateTime = 30f;


    //游戏结束存json，开始需要检测json，判断是否有第二关的按钮
    private void Awake()
    {
        // 设置json文件路径
        jsonFilePath = Path.Combine(Application.dataPath, "Resources/data.json");
        if (!File.Exists(jsonFilePath))
        {
            Debug.LogWarning("JSON file not found, creating a new one.");
            CreateEmptyJsonFile();
        }
        LoadJsonData();
    }
    void Start()
    {



        gameStateType = GameStateType.GameStart;
        GameStateSet();
    }
    private void OnDisable()
    {

    }

    public bool jsonSecond = false;
    private bool whetherUnlock=false;//第二关解锁标志
    [SerializeField] 
    DialogueTreeController treeSecondC;

    public bool isFirst = false;
    public bool isSecond = false;

    void Update()
    {
        //游戏开始
        if (gameStateType== GameStateType.TheFirstPass|| gameStateType == GameStateType.TheSecondPass)
        {
            KeepTime();
            Score();
            UpdateCounterText();
        }

        if (ScoreManagement.Instance.TotalScore>=1000 && whetherUnlock==false)
        {
            whetherUnlock = true;
            jsonSecond = true;
            //用第二套怪物生成（多种怪，根据积分）
            Debug.Log("第二关解锁了！！！");
        }else if (ScoreManagement.Instance.TotalScore >= 0 && ScoreManagement.Instance.TotalScore <= 1000)
        {
            whetherUnlock = false;
        }


        //第一关结束
        if (gameStateType == GameStateType.TheFirstPass && currentTime==0 && whetherUnlock==false)//第一关结束分值不够，直接结束
        {
            getTotalScore = ScoreManagement.Instance.TotalScore;
            gameStateType = GameStateType.GameEnd;
            GameStateSet();
            InterfaceManagement.Instance.OpenGameOverPlane();

        }
        if (gameStateType == GameStateType.TheFirstPass && currentTime == 0 && whetherUnlock == true)//第一关结束分值够了，进入第二关
        {
            PlayS();
        }
        //还需要一个直接进入的方法

        //第二关结束
        if (gameStateType == GameStateType.TheSecondPass&& currentTime==0)//初始化获取json数据判断
        {
            getTotalScore = ScoreManagement.Instance.TotalScore;
            gameStateType = GameStateType.GameEnd;
            GameStateSet();
            InterfaceManagement.Instance.OpenGameOverPlane();
        }

    }

    #region 计时
    public TMP_Text timerText; // 引用到你的Text UI组件
    public float currentTime = 180f; // 3分钟的计时
    //计时
    public void KeepTime()
    {
        // 每帧减少时间
        currentTime -= Time.deltaTime;

        // 更新UI文本显示当前剩余时间
        timerText.text = FormatTime(currentTime);

        // 当计时结束时的操作
        if (currentTime <= 0)
        {
            currentTime = 0;
            // 在这里可以添加计时结束后的逻辑
            timerText.text = "00:00";
        }
    }

    // 将时间格式化为mm:ss
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{00:00}:{01:00}", minutes, seconds);
    }
    #endregion

    //加减时间交互
    public void AddT(float additionalTime)
    {
        currentTime += additionalTime;
    }

    #region 计分计数
    public TMP_Text ScoerText;
    //打怪计数
    // 引用到你的Text UI元素
    public TMP_Text counterText;
    // 总数
    private int totalCount = 50;
    // 当前计数
    public int currentCount = 0;
    //得分计算
    public void Score()
    {
        if (ScoreManagement.Instance.TotalScore <= 0)
        {
            ScoreManagement.Instance.TotalScore = 0;
        }
        //TotalScore = addScoer + subtractScoer;
        ScoerText.text = ScoreManagement.Instance.TotalScore.ToString();
    }

    //计数
    // 更新Text内容的方法
    private void UpdateCounterText()
    {
        if (currentCount >= totalCount)
        {
            currentCount = totalCount;
        }
        counterText.text = $"{currentCount}/{totalCount}";
    }
    #endregion


    #region 道具在5x5范围内随机生成
    public GameObject parentT;
    //道具生成
    // 调用此方法以开始/停止生成
    public void ToggleSpawn(bool spawn)
    {
        shouldSpawn = spawn;

        if (spawn)
        {
            StartCoroutine(SpawnRandomCube());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator SpawnRandomCube()
    {
        while (shouldSpawn)
        {
            // 随机选择一个预制体
            GameObject prefabToSpawn = cubePrefabs[Random.Range(0, cubePrefabs.Length)];

            // 在-2.5到2.5之间生成一个随机位置，因为spawnArea是5
            Vector3 randomPosition = new Vector3(Random.Range(-spawnArea / 2, spawnArea / 2), 0, Random.Range(-spawnArea / 2, spawnArea / 2));

            // 在随机位置生成选定的预制体
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity, parentT.transform);

            float waitTime = Random.Range(minGenerateTime, maxGenerateTime);
            Debug.Log("等待"+ waitTime);
            // 等待30秒
            yield return new WaitForSeconds(waitTime);
        }
    }
    #endregion


    #region 游戏状态
    //游戏状态
    public void GameStateSet()
    {
        switch (gameStateType)
        {
            case GameStateType.GameStart:
                // 游戏开始时的逻辑------游戏进行前的初始化
                playerManager.canAttack = false;
                player.SetActive(false);
                currentTime = 180;
                ScoreManagement.Instance.TotalScore = 0;
                currentCount = 0;
                enemy.SetActive(false);
                enemySecond.SetActive(false);

                break;
            case GameStateType.TheFirstPass:
                // 游戏进行中的逻辑------点击开始游戏按钮进行游戏------(第一关)
                playerManager.canAttack = true;
                player.SetActive(true);
                enemy.SetActive(true);
                enemySecond.SetActive(false);


                //打开游戏UI面板
                //开始计时、计分
                //播放音乐
                //开始怪物生成

                break;

            case GameStateType.TheSecondPass:
                // ----------------------------------------------------(第二关)
                playerManager.canAttack = true;
                player.SetActive(true);
                enemy.SetActive(false);
                enemySecond.SetActive(true);
                break;
            case GameStateType.GameEnd:
                // 游戏结束时的逻辑------时间等于0结束游戏
                playerManager.canAttack = false;
                player.SetActive(false);
                enemy.SetActive(false);
                enemySecond.SetActive(false);

                //弹出结束面板
                //结束怪物生成------CancelInvoke(“怪物生成方法”);
                //角色关闭，重置

                //重新开始---》开始游戏的初始化（GameStart）---》游戏进行（Playing）
                //Debug.Log("游戏结束！");
                // 可以在此处添加重置游戏状态等逻辑
                break;
        }
    }
    #endregion


    #region 直接进入第二关
    public void PlayS()//进入第二关
    {
        gameStateType = GameStateType.GameStart;
        GameStateSet();
        InterfaceManagement.Instance.CloseAllPlane();
        CameraPositionRecorderEasy.Instance.MoveMainCameraToRecordedPosition(0);
        treeSecondC.StartDialogue();
    }
    public void PlaySecondGame()//对话完后调用的
    {
        CameraPositionRecorderEasy.Instance.MoveMainCameraToRecordedPosition(1);
        gameStateType = GameStateType.TheSecondPass;
        GameStateSet();
        InterfaceManagement.Instance.OpenGameUIPlane();
    }
    #endregion


    #region JSON的使用
    [SerializeField]
    private string jsonFilePath; // 文件路径
    public List<PersonData> personList = new List<PersonData>();
    private DataContainer dataContainer;
    void CreateEmptyJsonFile()
    {
        dataContainer = new DataContainer { entries = new PersonData[0] }; // 初始化一个空的数据容器
        string initialJson = JsonUtility.ToJson(dataContainer, true); // 格式化输出
        Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath)); // 确保目录存在
        File.WriteAllText(jsonFilePath, initialJson);
    }

    void LoadJsonData()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            dataContainer = JsonUtility.FromJson<DataContainer>(jsonData);
            // 将entries数组转换为List<PersonData>（如果你需要的话）
            personList = new List<PersonData>(dataContainer.entries);
        }
        else
        {
            Debug.LogError("Failed to create the JSON file. Please check the path.");
        }
    }

    // 添加数据的方法
    public void AddPersonData(string name, bool jsonSecond)
    {
        // 创建一个新的PersonData实例
        PersonData newData = new PersonData
        {
            name = name,
            jsonSecond = jsonSecond
        };

        // 将新的PersonData实例添加到列表中
        personList.Add(newData);

        // 打印确认信息
        Debug.Log("Added: " + newData.name + ", " + newData.jsonSecond);
    }

    // 根据名字查找并返回对应的PersonData信息
    public PersonData FindPersonByName(string nameToFind)
    {
        // 使用FirstOrDefault查找第一个匹配的名字
        // 如果没有找到匹配项，则返回null
        var foundPerson = personList.FirstOrDefault(person => person.name == nameToFind);

        return foundPerson;
    }

    // 序列化List<PersonData>到JSON字符串的方法
    public void SerializePersonDataList(List<PersonData> personDataList)
    {
        // 将List<PersonData>转换为PersonData[]，因为DataContainer期望的是数组
        PersonData[] entriesArray = personDataList.ToArray();

        // 创建DataContainer实例并赋值
        DataContainer dataContainer = new DataContainer
        {
            entries = entriesArray
        };

        // 使用JsonUtility将DataContainer实例序列化为JSON字符串
        string initialJ = JsonUtility.ToJson(dataContainer, true); // 第二个参数为true会美化输出的JSON字符串
        File.WriteAllText(jsonFilePath, initialJ);
    }
    #endregion
}


[System.Serializable]
public class PersonData
{
    public string name;
    public bool jsonSecond;
}

[System.Serializable]
public class DataContainer
{
    public PersonData[] entries;
}

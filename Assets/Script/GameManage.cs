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
    public int getTotalScore;//�÷ֽ�����ʾ�ĵ÷�

    // �������cubeԤ����
    public GameObject[] cubePrefabs;

    // ��������Ĵ�С
    public float spawnArea = 5f;

    // ��ʼ�������Э�̵ı�־
    private bool shouldSpawn = false;

    //���ɼ��ʱ�䷶Χ
    public float minGenerateTime = 10f;
    public float maxGenerateTime = 30f;


    //��Ϸ������json����ʼ��Ҫ���json���ж��Ƿ��еڶ��صİ�ť
    private void Awake()
    {
        // ����json�ļ�·��
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
    private bool whetherUnlock=false;//�ڶ��ؽ�����־
    [SerializeField] 
    DialogueTreeController treeSecondC;

    public bool isFirst = false;
    public bool isSecond = false;

    void Update()
    {
        //��Ϸ��ʼ
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
            //�õڶ��׹������ɣ����ֹ֣����ݻ��֣�
            Debug.Log("�ڶ��ؽ����ˣ�����");
        }else if (ScoreManagement.Instance.TotalScore >= 0 && ScoreManagement.Instance.TotalScore <= 1000)
        {
            whetherUnlock = false;
        }


        //��һ�ؽ���
        if (gameStateType == GameStateType.TheFirstPass && currentTime==0 && whetherUnlock==false)//��һ�ؽ�����ֵ������ֱ�ӽ���
        {
            getTotalScore = ScoreManagement.Instance.TotalScore;
            gameStateType = GameStateType.GameEnd;
            GameStateSet();
            InterfaceManagement.Instance.OpenGameOverPlane();

        }
        if (gameStateType == GameStateType.TheFirstPass && currentTime == 0 && whetherUnlock == true)//��һ�ؽ�����ֵ���ˣ�����ڶ���
        {
            PlayS();
        }
        //����Ҫһ��ֱ�ӽ���ķ���

        //�ڶ��ؽ���
        if (gameStateType == GameStateType.TheSecondPass&& currentTime==0)//��ʼ����ȡjson�����ж�
        {
            getTotalScore = ScoreManagement.Instance.TotalScore;
            gameStateType = GameStateType.GameEnd;
            GameStateSet();
            InterfaceManagement.Instance.OpenGameOverPlane();
        }

    }

    #region ��ʱ
    public TMP_Text timerText; // ���õ����Text UI���
    public float currentTime = 180f; // 3���ӵļ�ʱ
    //��ʱ
    public void KeepTime()
    {
        // ÿ֡����ʱ��
        currentTime -= Time.deltaTime;

        // ����UI�ı���ʾ��ǰʣ��ʱ��
        timerText.text = FormatTime(currentTime);

        // ����ʱ����ʱ�Ĳ���
        if (currentTime <= 0)
        {
            currentTime = 0;
            // �����������Ӽ�ʱ��������߼�
            timerText.text = "00:00";
        }
    }

    // ��ʱ���ʽ��Ϊmm:ss
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{00:00}:{01:00}", minutes, seconds);
    }
    #endregion

    //�Ӽ�ʱ�佻��
    public void AddT(float additionalTime)
    {
        currentTime += additionalTime;
    }

    #region �Ʒּ���
    public TMP_Text ScoerText;
    //��ּ���
    // ���õ����Text UIԪ��
    public TMP_Text counterText;
    // ����
    private int totalCount = 50;
    // ��ǰ����
    public int currentCount = 0;
    //�÷ּ���
    public void Score()
    {
        if (ScoreManagement.Instance.TotalScore <= 0)
        {
            ScoreManagement.Instance.TotalScore = 0;
        }
        //TotalScore = addScoer + subtractScoer;
        ScoerText.text = ScoreManagement.Instance.TotalScore.ToString();
    }

    //����
    // ����Text���ݵķ���
    private void UpdateCounterText()
    {
        if (currentCount >= totalCount)
        {
            currentCount = totalCount;
        }
        counterText.text = $"{currentCount}/{totalCount}";
    }
    #endregion


    #region ������5x5��Χ���������
    public GameObject parentT;
    //��������
    // ���ô˷����Կ�ʼ/ֹͣ����
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
            // ���ѡ��һ��Ԥ����
            GameObject prefabToSpawn = cubePrefabs[Random.Range(0, cubePrefabs.Length)];

            // ��-2.5��2.5֮������һ�����λ�ã���ΪspawnArea��5
            Vector3 randomPosition = new Vector3(Random.Range(-spawnArea / 2, spawnArea / 2), 0, Random.Range(-spawnArea / 2, spawnArea / 2));

            // �����λ������ѡ����Ԥ����
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity, parentT.transform);

            float waitTime = Random.Range(minGenerateTime, maxGenerateTime);
            Debug.Log("�ȴ�"+ waitTime);
            // �ȴ�30��
            yield return new WaitForSeconds(waitTime);
        }
    }
    #endregion


    #region ��Ϸ״̬
    //��Ϸ״̬
    public void GameStateSet()
    {
        switch (gameStateType)
        {
            case GameStateType.GameStart:
                // ��Ϸ��ʼʱ���߼�------��Ϸ����ǰ�ĳ�ʼ��
                playerManager.canAttack = false;
                player.SetActive(false);
                currentTime = 180;
                ScoreManagement.Instance.TotalScore = 0;
                currentCount = 0;
                enemy.SetActive(false);
                enemySecond.SetActive(false);

                break;
            case GameStateType.TheFirstPass:
                // ��Ϸ�����е��߼�------�����ʼ��Ϸ��ť������Ϸ------(��һ��)
                playerManager.canAttack = true;
                player.SetActive(true);
                enemy.SetActive(true);
                enemySecond.SetActive(false);


                //����ϷUI���
                //��ʼ��ʱ���Ʒ�
                //��������
                //��ʼ��������

                break;

            case GameStateType.TheSecondPass:
                // ----------------------------------------------------(�ڶ���)
                playerManager.canAttack = true;
                player.SetActive(true);
                enemy.SetActive(false);
                enemySecond.SetActive(true);
                break;
            case GameStateType.GameEnd:
                // ��Ϸ����ʱ���߼�------ʱ�����0������Ϸ
                playerManager.canAttack = false;
                player.SetActive(false);
                enemy.SetActive(false);
                enemySecond.SetActive(false);

                //�����������
                //������������------CancelInvoke(���������ɷ�����);
                //��ɫ�رգ�����

                //���¿�ʼ---����ʼ��Ϸ�ĳ�ʼ����GameStart��---����Ϸ���У�Playing��
                //Debug.Log("��Ϸ������");
                // �����ڴ˴����������Ϸ״̬���߼�
                break;
        }
    }
    #endregion


    #region ֱ�ӽ���ڶ���
    public void PlayS()//����ڶ���
    {
        gameStateType = GameStateType.GameStart;
        GameStateSet();
        InterfaceManagement.Instance.CloseAllPlane();
        CameraPositionRecorderEasy.Instance.MoveMainCameraToRecordedPosition(0);
        treeSecondC.StartDialogue();
    }
    public void PlaySecondGame()//�Ի������õ�
    {
        CameraPositionRecorderEasy.Instance.MoveMainCameraToRecordedPosition(1);
        gameStateType = GameStateType.TheSecondPass;
        GameStateSet();
        InterfaceManagement.Instance.OpenGameUIPlane();
    }
    #endregion


    #region JSON��ʹ��
    [SerializeField]
    private string jsonFilePath; // �ļ�·��
    public List<PersonData> personList = new List<PersonData>();
    private DataContainer dataContainer;
    void CreateEmptyJsonFile()
    {
        dataContainer = new DataContainer { entries = new PersonData[0] }; // ��ʼ��һ���յ���������
        string initialJson = JsonUtility.ToJson(dataContainer, true); // ��ʽ�����
        Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath)); // ȷ��Ŀ¼����
        File.WriteAllText(jsonFilePath, initialJson);
    }

    void LoadJsonData()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            dataContainer = JsonUtility.FromJson<DataContainer>(jsonData);
            // ��entries����ת��ΪList<PersonData>���������Ҫ�Ļ���
            personList = new List<PersonData>(dataContainer.entries);
        }
        else
        {
            Debug.LogError("Failed to create the JSON file. Please check the path.");
        }
    }

    // ������ݵķ���
    public void AddPersonData(string name, bool jsonSecond)
    {
        // ����һ���µ�PersonDataʵ��
        PersonData newData = new PersonData
        {
            name = name,
            jsonSecond = jsonSecond
        };

        // ���µ�PersonDataʵ����ӵ��б���
        personList.Add(newData);

        // ��ӡȷ����Ϣ
        Debug.Log("Added: " + newData.name + ", " + newData.jsonSecond);
    }

    // �������ֲ��Ҳ����ض�Ӧ��PersonData��Ϣ
    public PersonData FindPersonByName(string nameToFind)
    {
        // ʹ��FirstOrDefault���ҵ�һ��ƥ�������
        // ���û���ҵ�ƥ����򷵻�null
        var foundPerson = personList.FirstOrDefault(person => person.name == nameToFind);

        return foundPerson;
    }

    // ���л�List<PersonData>��JSON�ַ����ķ���
    public void SerializePersonDataList(List<PersonData> personDataList)
    {
        // ��List<PersonData>ת��ΪPersonData[]����ΪDataContainer������������
        PersonData[] entriesArray = personDataList.ToArray();

        // ����DataContainerʵ������ֵ
        DataContainer dataContainer = new DataContainer
        {
            entries = entriesArray
        };

        // ʹ��JsonUtility��DataContainerʵ�����л�ΪJSON�ַ���
        string initialJ = JsonUtility.ToJson(dataContainer, true); // �ڶ�������Ϊtrue�����������JSON�ַ���
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

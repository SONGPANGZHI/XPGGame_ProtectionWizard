using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterGenerationMagsT : MonoBehaviour
{
    [SerializeField]
    private List<Transform> monsterPointList;

    [SerializeField]
    private GameObject goodnessPrefab2;//掉血npc

    [SerializeField]
    private GameObject evilPrefab1;
    [SerializeField]
    private GameObject evilPrefab2;
    [SerializeField]
    private GameObject evilPrefab3;

    public int score = 0; // 分数

    private void OnEnable()
    {
        FristCreateMonter();
        InvokeRepeating("FristCreateMonter", 10f, 10f);
    }
    private void OnDisable()
    {
        CancelInvoke("FristCreateMonter");
    }
    //第一次生成怪物
    public void FristCreateMonter()
    {
        monsterPointList.ShuffleT();
        Set_MonsterSeneration();
    }

    [SerializeField]
    private int scorePhaseOne = 100;
    [SerializeField]
    private int scorePhaseTwo = 200;
    [SerializeField]
    private int scorePhaseTree = 300;

    bool firstBool;
    bool secondBool;
    bool thirdBool;
    public void Set_MonsterSeneration()
    {
        score = ScoreManagement.Instance.TotalScore;
        if (score >= 0 && score < scorePhaseOne)
        {
            int num = Random.Range(3,5);
            for (int i = 0; i < num; i++)
            {
                if (i == 0)
                {
                    GameObject ga = Instantiate(goodnessPrefab2, monsterPointList[i].transform);
                }
                else
                {
                    GameObject ga = Instantiate(evilPrefab1, monsterPointList[i].transform);
                }
            }
            if (!firstBool)
            {
                firstBool = true;
                ShowMonsterDescPlane(0);
            }

        }
        else if (score >= scorePhaseOne && score < scorePhaseTwo)
        {
            int a = Random.Range(3, 5);
            int b = Random.Range(1, 3);
            int num = a+b+1;
            for (int i = 0; i < num; i++)
            {
                if (i == 0)
                {
                    GameObject ga = Instantiate(goodnessPrefab2, monsterPointList[i].transform);
                }
                else if(i > 0 && i <= a)
                {
                    GameObject ga = Instantiate(evilPrefab1, monsterPointList[i].transform);
                }
                else if (i >= a && i<num)
                {
                    GameObject ga = Instantiate(evilPrefab2, monsterPointList[i].transform);
                }
            }
            if (!secondBool)
            {
                secondBool = true;
                ShowMonsterDescPlane(1);
            }

        }
        else if (score >= scorePhaseTwo)
        {
            int a = Random.Range(3, 5);
            int b = Random.Range(1, 3);
            int num = a + b + 4;
            for (int i = 0; i < num; i++)
            {
                if (i >= 0 && i < 2)
                {
                    GameObject ga = Instantiate(goodnessPrefab2, monsterPointList[i].transform);
                }
                else if (i >= 2 && i < a + 2)
                {
                    GameObject ga = Instantiate(evilPrefab1, monsterPointList[i].transform);
                }
                else if (i >= a + 2 && i < a + 2 + b)
                {
                    GameObject ga = Instantiate(evilPrefab2, monsterPointList[i].transform);
                }
                else if (i >= a + 2 + b)
                {
                    GameObject ga = Instantiate(evilPrefab3, monsterPointList[i].transform);
                }
            }
            if (!thirdBool)
            {
                thirdBool = true;
                ShowMonsterDescPlane(2);
            }
        }
    }

    //显示怪物提醒界面
    public void ShowMonsterDescPlane(int ID)
    {
        //Time.timeScale = 0;
        InterfaceManagement.Instance.OpenMonsterDescPlane(ID);

    }

}

//打乱List顺序
public static class ListExtensionsT
{
    public static void ShuffleT<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
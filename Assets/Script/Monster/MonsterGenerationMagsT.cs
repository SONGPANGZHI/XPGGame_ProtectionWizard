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

    [SerializeField]
    private int randomMin = 5;

    [SerializeField]
    private int randomMax = 8;

    [SerializeField]
    private int goodnessMonsterNum = 2;

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

        int goodID = RandomMonterType();
        if (goodID == 0)
        {
            //本轮多个善良怪
            SpawnMultipleMonsters();
        }
        else
            SpawnOneMonsters();

    }

    //2个npc的情况
    //生成多个善良怪
    public void SpawnMultipleMonsters()
    {
        score = ScoreManagement.Instance.TotalScore;
        if (score>=0 && score < 100)//只生成npc和一种怪
        {
            for (int i = 0; i < RandomMonterNum(); i++)
            {
                if (i < goodnessMonsterNum)
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
        //else if(score >= 100 && score < 200)
        //{
        //    for (int i = 0; i < RandomMonterNum(); i++)
        //    {
        //        if (i < goodnessMonsterNum)
        //        {
        //            GameObject ga = Instantiate(goodnessPrefab2, monsterPointList[i].transform);
        //        }
        //        else
        //        {
        //            GameObject ga = Instantiate(evilPrefab2, monsterPointList[i].transform);
        //        }
        //    }
        //}
        else if (score >= 100 && score < 200)
        {
            int num1 = RandomMonterNum();
            int a = num1 - goodnessMonsterNum;
            int cubeCount = Random.Range(1, a);
            for (int i = 0; i < num1; i++)
            {
                if (i < goodnessMonsterNum)//2
                {
                    GameObject ga = Instantiate(goodnessPrefab2, monsterPointList[i].transform);
                }
                else if(i>= goodnessMonsterNum && i< goodnessMonsterNum+ cubeCount)//2---(2+x)
                {
                    GameObject ga = Instantiate(evilPrefab1, monsterPointList[i].transform);
                }
                else if (i >= goodnessMonsterNum+ cubeCount && i < num1)//(2-x)---总数
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
        else if (score >= 200)
        {
            int num1 = RandomMonterNum();
            int num = num1 - goodnessMonsterNum;//6-2  4
            int a = Random.Range(1, num - 1);//（1,3）                1和2之间随机 2
            int circleCount = num - a;//4-2                 随到2     得到2
            int b = Random.Range(1, circleCount);//1---2    1
            int c = num - a - b;//                          1
            //0/1------2/3------4------5
            for (int i = 0; i < num1; i++)
            {
                if (i < goodnessMonsterNum)//2
                {
                    GameObject ga = Instantiate(goodnessPrefab2, monsterPointList[i].transform);
                }
                else if (i >= goodnessMonsterNum && i < goodnessMonsterNum + a)
                {
                    GameObject ga = Instantiate(evilPrefab1, monsterPointList[i].transform);
                }
                else if (i >= goodnessMonsterNum + a && i < goodnessMonsterNum + a+ b)
                {
                    GameObject ga = Instantiate(evilPrefab2, monsterPointList[i].transform);
                }
                else if (i >= goodnessMonsterNum + a + b && i < num1)
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

    //1个npc的情况

    bool firstBool;
    bool secondBool;
    bool thirdBool;



    //生成最少一个
    public void SpawnOneMonsters()
    {
        score = ScoreManagement.Instance.TotalScore;
        if (score >= 0 && score < 100)
        {
            for (int i = 0; i < RandomMonterNum(); i++)
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
        //else if(score >= 100 && score < 200)
        //{
        //    for (int i = 0; i < RandomMonterNum(); i++)
        //    {
        //        if (i == 0)
        //        {
        //            GameObject ga = Instantiate(goodnessPrefab2, monsterPointList[i].transform);
        //        }
        //        else
        //        {
        //            GameObject ga = Instantiate(evilPrefab2, monsterPointList[i].transform);
        //        }
        //    }
        //}
        else if (score >= 100 && score < 200)
        {
            int num1 = RandomMonterNum();//                5   5-1=4
            int a = num1 - 1;
            int cubeCount = Random.Range(1, a);//1到4之间   3
            //0------1/2/3------4/5
            for (int i = 0; i < num1; i++)
            {
                if (i == 0)
                {
                    GameObject ga = Instantiate(goodnessPrefab2, monsterPointList[i].transform);
                }
                else if(i>0&&i<= cubeCount)
                {
                    GameObject ga = Instantiate(evilPrefab1, monsterPointList[i].transform);
                }
                else if (i > cubeCount && i < num1)
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
        else if (score >= 200)
        {
            int num1 = RandomMonterNum();//5-1           4
            int num = num1 - 1;
            int a = Random.Range(1, num - 1);//1---3        1和2之间随机 2
            int circleCount = num - a;//4-2                 2
            int b = Random.Range(1, circleCount);//1---2    1
            int c = num - a - b;//                          1

            //0------(1/2/3)------(4)------(5)
            for (int i = 0; i < num1; i++)
            {
                if (i == 0)
                {
                    GameObject ga = Instantiate(goodnessPrefab2, monsterPointList[i].transform);
                }
                else if (i > 0 && i <= a)
                {
                    GameObject ga = Instantiate(evilPrefab1, monsterPointList[i].transform);
                }
                else if (i > a && i <= a+b)
                {
                    GameObject ga = Instantiate(evilPrefab2, monsterPointList[i].transform);
                }
                else if (i > a + b && i<num1)
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

    //public int randomMN;
    //第一次随机生成怪物数量
    public int RandomMonterNum()
    {
        int monterNum = Random.Range(randomMin, randomMax);
        return monterNum;
    }

    //随机善良、邪恶怪物  0为善良
    public int RandomMonterType()
    {
        int monterType = Random.Range(0, 2);
        if (monterType == 0)
        {
            DoubleHitManager.Instance.DebugColorRed("本轮有多个善良的小怪");
            return 0;
        }
        else
        {
            DoubleHitManager.Instance.DebugColorRed("本轮至少有一个善良的小怪");
            return 1;
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
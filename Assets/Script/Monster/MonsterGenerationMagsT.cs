using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterGenerationMagsT : MonoBehaviour
{
    [SerializeField]
    private List<Transform> monsterPointList;

    [SerializeField]
    private GameObject goodnessPrefab2;//��Ѫnpc

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

    public int score = 0; // ����

    private void OnEnable()
    {
        FristCreateMonter();
        InvokeRepeating("FristCreateMonter", 10f, 10f);
    }
    private void OnDisable()
    {
        CancelInvoke("FristCreateMonter");
    }
    //��һ�����ɹ���
    public void FristCreateMonter()
    {
        monsterPointList.ShuffleT();

        int goodID = RandomMonterType();
        if (goodID == 0)
        {
            //���ֶ��������
            SpawnMultipleMonsters();
        }
        else
            SpawnOneMonsters();

    }

    //2��npc�����
    //���ɶ��������
    public void SpawnMultipleMonsters()
    {
        score = ScoreManagement.Instance.TotalScore;
        if (score>=0 && score < 100)//ֻ����npc��һ�ֹ�
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
                else if (i >= goodnessMonsterNum+ cubeCount && i < num1)//(2-x)---����
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
            int a = Random.Range(1, num - 1);//��1,3��                1��2֮����� 2
            int circleCount = num - a;//4-2                 �浽2     �õ�2
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

    //1��npc�����

    bool firstBool;
    bool secondBool;
    bool thirdBool;



    //��������һ��
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
            int cubeCount = Random.Range(1, a);//1��4֮��   3
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
            int a = Random.Range(1, num - 1);//1---3        1��2֮����� 2
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
    //��һ��������ɹ�������
    public int RandomMonterNum()
    {
        int monterNum = Random.Range(randomMin, randomMax);
        return monterNum;
    }

    //���������а�����  0Ϊ����
    public int RandomMonterType()
    {
        int monterType = Random.Range(0, 2);
        if (monterType == 0)
        {
            DoubleHitManager.Instance.DebugColorRed("�����ж��������С��");
            return 0;
        }
        else
        {
            DoubleHitManager.Instance.DebugColorRed("����������һ��������С��");
            return 1;
        }

    }

    //��ʾ�������ѽ���
    public void ShowMonsterDescPlane(int ID)
    {
        //Time.timeScale = 0;
        InterfaceManagement.Instance.OpenMonsterDescPlane(ID);

    }

}

//����List˳��
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
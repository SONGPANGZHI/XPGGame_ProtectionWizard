using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerationMags : MonoBehaviour
{
    [SerializeField]
    private List<Transform> monsterPointList;

    [SerializeField]
    private GameObject goodnessPrefab;

    [SerializeField]
    private GameObject evilPrefab;

    [SerializeField]
    private int randomMin = 4;

    [SerializeField]
    private int randomMax = 7;

    [SerializeField]
    private int goodnessMonsterNum = 2;

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
        monsterPointList.Shuffle();

        int goodID = RandomMonterType();
        if (goodID == 0)
        {
            //本轮多个善良怪
            SpawnMultipleMonsters();
        }
        else
            SpawnOneMonsters();

    }

    //生成多个善良怪
    public void SpawnMultipleMonsters()
    {
        for (int i = 0; i < RandomMonterNum(); i++)
        {
            if (i < goodnessMonsterNum)
            {
                GameObject ga = Instantiate(goodnessPrefab, monsterPointList[i].transform);
            }
            else
            {
                GameObject ga = Instantiate(evilPrefab, monsterPointList[i].transform);
            }
        }
    }

    //生成最少一个
    public void SpawnOneMonsters()
    {
        for (int i = 0; i < RandomMonterNum(); i++)
        {
            if (i == 0)
            {
                GameObject ga = Instantiate(goodnessPrefab, monsterPointList[i].transform);
            }
            else
            {
                GameObject ga = Instantiate(evilPrefab, monsterPointList[i].transform);
            }
        }

    }

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

}

//打乱List顺序
public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
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


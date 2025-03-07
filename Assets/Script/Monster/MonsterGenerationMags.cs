using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MonsterGenerationMags : MonoBehaviour
{
    [SerializeField]
    private List<Transform> monsterPointList;

    [SerializeField]
    private GameObject goodnessPrefab;

    [SerializeField]
    private GameObject evilPrefab;

    public List<FirstLevelMonsterWave> monsterWaveList;

    public List<FirstTrans> FirstTransList;

    //怪物、npc生成
    public void CreateMonsterWave(int ID)
    {
        int monsterTotal = UnityEngine.Random.Range(monsterWaveList[ID].monsterMin, monsterWaveList[ID].monsterMax);
        int _npcNum = monsterTotal - monsterWaveList[ID].npcNum;
        monsterPointList.Shuffle();
        for (int i = 0; i < monsterTotal; i++)
        {
            if (i < _npcNum)
            {
                GameObject ga = Instantiate(evilPrefab, monsterPointList[i].transform);
            }
            else
            {
                GameObject ga = Instantiate(goodnessPrefab, monsterPointList[i].transform);
            }
        }

    }

    //生成第一波次的怪
    public void FirstMonster(int ID)
    {
        int monsterTotal = UnityEngine.Random.Range(monsterWaveList[ID].monsterMin, monsterWaveList[ID].monsterMax);
        int _npcNum = monsterTotal - monsterWaveList[ID].npcNum;
        int indexId = UnityEngine.Random.Range(0, FirstTransList.Count);

        for (int i = 0; i < monsterTotal; i++)
        {
            if (i < _npcNum)
            {
                GameObject ga = Instantiate(evilPrefab, FirstTransList[indexId].transPos[i].transform);
            }
            else
            {
                GameObject ga = Instantiate(goodnessPrefab, FirstTransList[indexId].transPos[i].transform);
            }
        }

    }


    private void OnEnable()
    {
        //InvokeRepeating(() => CreateMonsterWave(0), 10f, 10f);
        FirstMonster(2);
        InvokeRepeating("CreateMonter", 10f, 10f);
    }

    public void CreateMonter()
    {
        if (monsterWaveList[0].monsterWave)
            CreateMonsterWave(0);
        if (monsterWaveList[1].monsterWave)
            CreateMonsterWave(1);
        if (monsterWaveList[2].monsterWave)
            FirstMonster(2);
    }

    private void OnDisable()
    {
        CancelInvoke("CreateMonter");
    }

    [Serializable]
    public class FirstLevelMonsterWave
    {
        public bool monsterWave;           //怪物波次
        public int npcNum;                 //npc数量
        public int monsterMin;             //最小数量
        public int monsterMax;             //最大数量
    }

    [Serializable]
    public class FirstTrans
    {
        public List<Transform> transPos;
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


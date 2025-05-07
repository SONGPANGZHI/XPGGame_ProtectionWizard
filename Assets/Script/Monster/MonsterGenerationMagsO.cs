using System;
using System.Collections.Generic;
using UnityEngine;
using static MonsterAttributes;

public class MonsterGenerationMagsO : MonoBehaviour
{
    [SerializeField]
    private List<Transform> monsterPointList;

    [SerializeField]
    private GameObject goodnessPrefab;

    [SerializeField]
    private GameObject evilPrefab;

    private readonly int[] waveMonsterCounts = { 3, 3, 4, 4, 5, 5, 6, 7, 7, 8, 8, 9 };
    private int currentWave = 0;
    private float spawnInterval = 15f;

    [SerializeField]
    private List<GameObject> currentWaveEvilMonsters = new List<GameObject>();  //跟踪当前波次的evil怪物

    public void CreateMonsterWave()
    {
        if (currentWave >= waveMonsterCounts.Length)
            return;

        currentWaveEvilMonsters.Clear();

        int totalMonsters = waveMonsterCounts[currentWave];
        int evilCount = (totalMonsters / 2) + 1;
        int goodnessCount = totalMonsters - evilCount;

        monsterPointList.Shuffle();

        for (int i = 0; i < totalMonsters; i++)
        {
            if (currentWave==0)
            {
                GameObject ga = Instantiate(evilPrefab, monsterPointList[i].transform);
                currentWaveEvilMonsters.Add(ga);  //记录生成的evil怪物

                //监听怪物销毁事件
                var monsterAttr = ga.GetComponent<MonsterAttributes>();
                if (monsterAttr != null)
                {
                    monsterAttr.OnMonsterDestroyed += CheckWaveCompletion;
                }
            }
            else
            {
                if (i < evilCount)
                {
                    GameObject ga = Instantiate(evilPrefab, monsterPointList[i].transform);
                    currentWaveEvilMonsters.Add(ga);  //记录生成的evil怪物

                    //监听怪物销毁事件
                    var monsterAttr = ga.GetComponent<MonsterAttributes>();
                    if (monsterAttr != null)
                    {
                        monsterAttr.OnMonsterDestroyed += CheckWaveCompletion;
                    }
                }
                else
                {
                    GameObject ga = Instantiate(goodnessPrefab, monsterPointList[i].transform);
                    // 计算当前是第几个goodness怪物（从0开始）
                    int goodnessIndex = i - evilCount;
                    // 基础延迟3秒，每个后续的怪物多延迟1秒
                    ga.GetComponent<MonsterAttributes>().transformDelay += goodnessIndex;
                }
            }

        }

        currentWave++;
    }

    //检查是否列表中所有evil怪物都被清理
    private void CheckWaveCompletion(GameObject monster)
    {
        if (currentWaveEvilMonsters.Contains(monster))
        {
            currentWaveEvilMonsters.Remove(monster);
        }

        // 检查剩余的怪物中是否还有Evil标签的
        bool hasEvil = false;
        foreach (var m in currentWaveEvilMonsters)
        {
            MonterType mT = m.GetComponent<MonsterAttributes>().GetMonterType();
            if (m != null && mT == MonterType.Evil)
            {
                hasEvil = true;
                break;
            }
        }

        if (!hasEvil)
        {
            ScoreManagement.Instance.GetScore(20);
            IncentiveSystemUI.Instance.Export_EncouragingContentEn(10);
        }
    }

    private void OnEnable()
    {
        currentWave = 0;
        CreateMonsterWave();
        InvokeRepeating(nameof(CreateMonsterWave), spawnInterval, spawnInterval);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(CreateMonsterWave));
    }
}

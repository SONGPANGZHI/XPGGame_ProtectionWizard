using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerationMagsO : MonoBehaviour
{
    [SerializeField]
    private List<Transform> monsterPointList;

    [SerializeField]
    private GameObject goodnessPrefab;

    [SerializeField]
    private GameObject evilPrefab;

    private readonly int[] waveMonsterCounts = { 2, 3, 3, 4, 4, 5, 6, 7, 7, 8, 8, 9 };
    private int currentWave = 0;
    private float spawnInterval = 15f;

    public void CreateMonsterWave()
    {
        if (currentWave >= waveMonsterCounts.Length)
            return;

        int totalMonsters = waveMonsterCounts[currentWave];
        int evilCount = (totalMonsters / 2) + 1;
        int goodnessCount = totalMonsters - evilCount;

        monsterPointList.Shuffle();

        for (int i = 0; i < totalMonsters; i++)
        {
            if (i < evilCount)
            {
                GameObject ga = Instantiate(evilPrefab, monsterPointList[i].transform);
            }
            else
            {
                GameObject ga = Instantiate(goodnessPrefab, monsterPointList[i].transform);
            }
        }

        currentWave++;
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

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterIntroductionPlane : MonoBehaviour
{
    [SerializeField]
    private Image monsterIcon;
    [SerializeField]
    private TMP_Text monsterDesc;

    public List<NewMonster> newMonstersList;

    //初始化

    public void InitMonsterDesc(int ID)
    {
        monsterIcon.sprite = newMonstersList[ID].MonsterIcon;
        monsterDesc.text = newMonstersList[ID].monsterDesc;
        Invoke("CloseMonsterDesc", 3F);
    }

    //3s 关闭界面
    public void CloseMonsterDesc()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }


    [Serializable]
    public class NewMonster
    {
        public string monsterDesc;
        public Sprite MonsterIcon;
    }
}



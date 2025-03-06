using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManagement : MonoBehaviour
{
    public static InterfaceManagement Instance;

    public GameObject startPlane;
    public GameObject loadingPlane;
    public GameObject gameUIPlane;
    public GameObject gameOverPlane;
    public GameObject userLoginPlane;
    public GameObject selectLevelPlane;
    public GameObject monsterDescPlane;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        OpenStarPlane();
    }

    //打开开始界面
    public void OpenStarPlane()
    {
        CloseAllPlane();
        startPlane.SetActive(true);
        startPlane.GetComponent<StartPlane>().JudgeUserUnlockLevel();
    }

    //打开加载界面
    public void OpenLoadPlane()
    {
        CloseAllPlane();
        loadingPlane.SetActive(true);
    }

    //打开游戏界面
    public void OpenGameUIPlane()
    {
        CloseAllPlane();
        gameUIPlane.SetActive(true);
    }

    //打开游戏结束界面
    public void OpenGameOverPlane()
    {
        CloseAllPlane();
        SoundManagement.Instance.PlaySFX(4);
        gameOverPlane.SetActive(true);
    }

    //用户登录界面
    public void OpenUserLoginPlane()
    {
        CloseAllPlane();
        userLoginPlane.SetActive(true);
    }

    //打开关卡选择界面
    public void OpenSelectLevelPlane()
    {
        CloseAllPlane();
        selectLevelPlane.SetActive(true);
    }

    //打开怪物介绍
    public void OpenMonsterDescPlane(int ID)
    {
        monsterDescPlane.SetActive(true);
        monsterDescPlane.GetComponent<MonsterIntroductionPlane>().InitMonsterDesc(ID);
    }

    //关闭所有界面
    public void CloseAllPlane()
    {
        startPlane.SetActive(false);
        loadingPlane.SetActive(false);
        gameUIPlane.SetActive(false);
        gameOverPlane.SetActive(false);
        userLoginPlane.SetActive(false);
        selectLevelPlane.SetActive(false);
        monsterDescPlane.SetActive(false);
    }
}

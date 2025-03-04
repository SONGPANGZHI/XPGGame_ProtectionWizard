using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelPlane : MonoBehaviour
{
    [SerializeField]
    private Button easyBTN;
    [SerializeField]
    private Button difficulty_BTN;

    private void Awake()
    {
        easyBTN.onClick.AddListener(EasyClick);
        difficulty_BTN.onClick.AddListener(DifficultyClick);
    }

    //简单模式
    public void EasyClick()
    {
        GameManage.Instance.isFirst = true;
        GameManage.Instance.isSecond = false;
        CameraPositionRecorderEasy.Instance.MoveMainCameraToRecordedPosition(0);
        GameManage.Instance.gameStateType = GameManage.GameStateType.GameStart;
        GameManage.Instance.GameStateSet();
        InterfaceManagement.Instance.OpenLoadPlane();
    }

    //困难模式
    public void DifficultyClick()
    {
        GameManage.Instance.isFirst = false;
        GameManage.Instance.isSecond = true;
        GameManage.Instance.PlayS();
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

public class LoadingPlane : MonoBehaviour
{
    [SerializeField]
    private Image loadSliderValue;          //进度条

    [SerializeField]
    private TMP_Text loadNumber_Text;       //文本文字

    #region 弃用
    //private float fixedTime = 0.0f;
    //private float target = 1.0f;
    //private float fixedDeltaTime = 0.0f;
    #endregion

    private void FixedUpdate()
    {

        #region 过场UI（弃用）
        //if (loadSliderValue.fillAmount == 1)
        //    return;

        //fixedTime = Mathf.Lerp(fixedTime, target, Time.fixedDeltaTime / (target - fixedDeltaTime));
        //fixedDeltaTime += Time.fixedDeltaTime;
        //loadSliderValue.fillAmount = fixedDeltaTime;
        //if (fixedDeltaTime >= 1)
        //{
        //    fixedDeltaTime = 1;
        //    Invoke("PlayGame",1f);
        //}

        //loadNumber_Text.text = Math.Truncate(fixedDeltaTime * 100) + "%";
        #endregion
    }

    [SerializeField] DialogueTreeController treeOneC;
    [SerializeField] Blackboard NpcB;
    //检测是否对话过，对话过就直接跳过对话
    private void OnEnable()
    {
        //在这里进行一次判断，来显示不同的对话，或是不显示对话
        Debug.Log(NpcB.GetVariableValue<bool>("EndS"));
        if (NpcB.GetVariableValue<bool>("EndS"))
        {
            PlayGame();
        }
        else
        {
            treeOneC.StartDialogue();
        }

    }

    //开始游戏
    public void PlayGame()
    {
        CameraPositionRecorderEasy.Instance.MoveMainCameraToRecordedPosition(1);
        //Debug.LogError("开始游戏");
        GameManage.Instance.gameStateType = GameManage.GameStateType.TheFirstPass;
        GameManage.Instance.GameStateSet();

        #region 弃用
        //loadSliderValue.fillAmount = 0;
        //fixedTime = 0.0f;
        //fixedDeltaTime = 0.0f;
        //GameManage.Instance.ToggleSpawn(true);//道具生成
        #endregion

        InterfaceManagement.Instance.OpenGameUIPlane();
    }

}

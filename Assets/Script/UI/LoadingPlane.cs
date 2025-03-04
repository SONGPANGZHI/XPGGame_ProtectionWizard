using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

public class LoadingPlane : MonoBehaviour
{
    [SerializeField]
    private Image loadSliderValue;          //������

    [SerializeField]
    private TMP_Text loadNumber_Text;       //�ı�����

    #region ����
    //private float fixedTime = 0.0f;
    //private float target = 1.0f;
    //private float fixedDeltaTime = 0.0f;
    #endregion

    private void FixedUpdate()
    {

        #region ����UI�����ã�
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
    //����Ƿ�Ի������Ի�����ֱ�������Ի�
    private void OnEnable()
    {
        //���������һ���жϣ�����ʾ��ͬ�ĶԻ������ǲ���ʾ�Ի�
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

    //��ʼ��Ϸ
    public void PlayGame()
    {
        CameraPositionRecorderEasy.Instance.MoveMainCameraToRecordedPosition(1);
        //Debug.LogError("��ʼ��Ϸ");
        GameManage.Instance.gameStateType = GameManage.GameStateType.TheFirstPass;
        GameManage.Instance.GameStateSet();

        #region ����
        //loadSliderValue.fillAmount = 0;
        //fixedTime = 0.0f;
        //fixedDeltaTime = 0.0f;
        //GameManage.Instance.ToggleSpawn(true);//��������
        #endregion

        InterfaceManagement.Instance.OpenGameUIPlane();
    }

}

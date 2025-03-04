using UnityEngine;
using UnityEngine.UI;

public class StartPlane : MonoBehaviour
{
    [SerializeField]
    private Button startBTN;
    [SerializeField]
    private Button exitBTN;
    [SerializeField]
    private Button userBTN;
    [SerializeField]
    private Button secondBTN;

    private void Awake()
    {
        startBTN.onClick.AddListener(StartClick);

        secondBTN.onClick.AddListener(StartSecondClick);

        userBTN.onClick.AddListener(UserLoginClick);

        exitBTN.onClick.AddListener(ExitClick);
    }


    [SerializeField]
    private bool isEnableSecond = false;
    [SerializeField]
    private string isName= "����";
    private void Start()
    {
        PersonData result = GameManage.Instance.FindPersonByName(isName);
        if (result != null)
        {
            isEnableSecond = result.jsonSecond;
        }
        if (isEnableSecond)
        {
            secondBTN.gameObject.SetActive(true);
        }

    }
    //��ʼ
    public void StartClick()
    {
        GameManage.Instance.isFirst = true;
        GameManage.Instance.isSecond = false;
        CameraPositionRecorderEasy.Instance.MoveMainCameraToRecordedPosition(0);
        GameManage.Instance.gameStateType = GameManage.GameStateType.GameStart;
        GameManage.Instance.GameStateSet();
        InterfaceManagement.Instance.OpenLoadPlane();
    }

    //ѡ��ؿ�����
    public void StartSecondClick()
    {
        //InterfaceManagement.Instance.OpenSelectLevelPlane();
        GameManage.Instance.isFirst = false;
        GameManage.Instance.isSecond = true;
        GameManage.Instance.PlayS();
    }

    //�û���¼
    public void UserLoginClick()
    {
        InterfaceManagement.Instance.OpenUserLoginPlane();
    }

    //�ж��û��Ƿ�����ڶ���
    public void JudgeUserUnlockLevel()
    {
        if (UserLoginPlane.userNameLabel == "" || GameManage.Instance.FindPersonByName(UserLoginPlane.userNameLabel) == null)
            return;

        if (GameManage.Instance.FindPersonByName(UserLoginPlane.userNameLabel).jsonSecond)
            secondBTN.gameObject.SetActive(true);
        else
            secondBTN.gameObject.SetActive(false);
    }

    //�˳�
    public void ExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

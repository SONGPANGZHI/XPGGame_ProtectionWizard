using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPlane : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score_Text;

    [SerializeField]
    private TMP_Text totalTime;

    [SerializeField]
    private Button restartBTN;

    [SerializeField]
    private Button exitBTN;

    [SerializeField]
    private Button mainBTN;

    private void Awake()
    {
        restartBTN.onClick.AddListener(RestartClick);
        exitBTN.onClick.AddListener(ExitClick);
        mainBTN.onClick.AddListener(MainClick);
    }

    private void Start()
    {
        GetTotalScore();
    }

    //获取总得分
    public void GetTotalScore()
    {
        score_Text.text = GameManage.Instance.getTotalScore.ToString();
    }
    //返回主界面
    public void MainClick()
    {
        InterfaceManagement.Instance.OpenStarPlane();
    }

    //获取总时间
    public void GetTotalTime() 
    {
    
    }  

    //重新开始游戏
    public void RestartClick()
    {
        if (GameManage.Instance.isSecond==true&& GameManage.Instance.isFirst==false)
        {
            GameManage.Instance.PlayS();
        }
        else if(GameManage.Instance.isSecond == false && GameManage.Instance.isFirst == true)
        {
            CameraPositionRecorderEasy.Instance.MoveMainCameraToRecordedPosition(0);
            GameManage.Instance.gameStateType = GameManage.GameStateType.GameStart;
            GameManage.Instance.GameStateSet();
            InterfaceManagement.Instance.OpenLoadPlane();
        }

    }

    //退出游戏
    public void ExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}

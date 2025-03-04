using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timer_Text;

    [SerializeField]
    private TMP_Text monterNum_Text;

    [SerializeField]
    private TMP_Text score_Text;

    //数据初始化
    public void GameUIInit()
    {
        UpdateScroe();
        HitMonterNum();
    }

    //刷新时间
    

    //获取怪物总数
    public int GetMontserNum()
    {
        return 0;
    }

    //刷新击打怪物数量
    public void HitMonterNum()
    { 
        int hitNum = 0;
        monterNum_Text.text = hitNum + "/" + GetMontserNum();
    }

    //刷新得分
    public void UpdateScroe()
    {
        score_Text.text = ScoreManagement.Instance.TotalScore.ToString();
    }

}

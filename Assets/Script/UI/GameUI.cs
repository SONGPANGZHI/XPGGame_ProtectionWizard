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

    //���ݳ�ʼ��
    public void GameUIInit()
    {
        UpdateScroe();
        HitMonterNum();
    }

    //ˢ��ʱ��
    

    //��ȡ��������
    public int GetMontserNum()
    {
        return 0;
    }

    //ˢ�»����������
    public void HitMonterNum()
    { 
        int hitNum = 0;
        monterNum_Text.text = hitNum + "/" + GetMontserNum();
    }

    //ˢ�µ÷�
    public void UpdateScroe()
    {
        score_Text.text = ScoreManagement.Instance.TotalScore.ToString();
    }

}

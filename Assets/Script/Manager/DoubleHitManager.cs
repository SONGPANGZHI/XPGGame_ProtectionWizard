using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoubleHitManager : MonoBehaviour
{
    public static DoubleHitManager Instance;

    public int doubleHitCount = 0;
    public float doubleHitScore;
    private const int BASE_SCORE = 10;  // 基础分数
    private const int MAX_BONUS = 5;    // 最大额外加分
    
    public GameObject doubleHit_UI;
    public GameObject doubleHit_FVX;
    public TMP_Text comboText;              // 连击次数显示
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    //连击次数判断
    public void JudgeDoubleHit(int baseScore)
    {
        // 计算额外加分(上限+5分)
        int bonusScore = Mathf.Min(doubleHitCount - 3, MAX_BONUS);
        bonusScore = bonusScore < 0 ? 0 : bonusScore;
        
        doubleHitScore = baseScore + bonusScore;

        if(!doubleHit_UI.activeSelf)
        {
            Invoke("DelayTime", 0.5f);
        }
        
        // 更新连击显示
        if(comboText != null)
        {
            comboText.text = $"连击 x{doubleHitCount}";
        }
    }

    public void DelayTime()
    {
        doubleHit_UI.SetActive(true);
        //doubleHit_FVX.SetActive(true);
    }

    //清除连击次数(击中NPC时调用)
    public void ClearDoubleHitCount()
    {
        doubleHitCount = 0;
        doubleHitScore = 0;
        IncentiveSystemUI.Instance.Export_EncouragingContentEn(5);
        doubleHit_UI.SetActive(false);
        //doubleHit_FVX.SetActive(false);
        if(comboText != null)
        {
            comboText.text = "";
        }
        DebugColorYellow("连击数清零：" + doubleHitCount);
    }


    public void ClearDHC()//清除连击
    {
        doubleHitCount = 0;
        doubleHit_UI.SetActive(false);
        //doubleHit_FVX.SetActive(false);
        DebugColorYellow("连击数清零：" + doubleHitCount);
    }

    //累计连击次数
    public int DoubleHitTimes()
    {
        doubleHitCount += 1;
        DebugColorYellow("输出击打数：" + doubleHitCount);
        return doubleHitCount;
    }


    public float RewardT = 10f;
    public Image sl;

    void Update()
    {
        if (doubleHit_UI.activeSelf)
        {
            SetSlider();
        }
            
    }

    public void SetSlider()
    {
        RewardT -= Time.deltaTime;
        if (RewardT <= 0)
        {
            RewardT = 0;
        }
        sl.fillAmount = RewardT / 10f;
    }


    #region DebugLog颜色

    /// <summary>
    /// 输出红色Log
    /// </summary>
    public void DebugColorRed(string _log)
    {
        Debug.Log(string.Format("<color=#FF3434>{0}</color>", _log));
    }

    /// <summary>
    /// 输出黄色Log
    /// </summary>
    public void DebugColorYellow(string _log)
    {
        Debug.Log(string.Format("<color=#F5FF34>{0}</color>", _log));
    }

    #endregion
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoubleHitManager : MonoBehaviour
{
    public static DoubleHitManager Instance;

    public int doubleHitCount = 0;
    public int limitTime = 10;               //连击时间限制 暂定10s
    public float doubleHitScore;

    public GameObject doubleHit_UI;
    public SpriteRenderer value;

    private float times_3 = 1.2f;
    private float times_5 = 1.5f;
    private float times_8 = 2f;
    private bool timewait = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    //连击次数判断
    public void JudgeDoubleHit(int a)
    {
        doubleHitScore = 0;
        if (doubleHitCount > 3 && doubleHitCount <= 5)
        {
            doubleHitScore = a * times_3;
        }
        else if (doubleHitCount > 5 && doubleHitCount <= 8)
        {
            doubleHitScore = a * times_5;
        }
        else if (doubleHitCount > 8)
        {
            doubleHitScore = a * times_8;
        }
        ScoreManagement.Instance.GetScore((int)doubleHitScore);

        if (!timewait)
        {
            doubleHit_UI.SetActive(true);
            RewardT = 10f;
            sl.fillAmount = 1;
            DebugColorRed("开始计时");
            timewait = true;
            StartCoroutine(TimeFunc(limitTime));

        }
    }

    //连击计时器 暂定10s
    private IEnumerator TimeFunc(float TimeCount)
    {
        do
        {
            yield return new WaitForSeconds(1);
            TimeCount -= 1;
        } while (TimeCount > 0);

        doubleHitScore = 0;
        ClearDoubleHitCount();
        timewait = false;
        DebugColorRed("重新计算连击");
    }

    //清除连击次数
    public void ClearDoubleHitCount()
    {
        doubleHitCount = 0;
        doubleHit_UI.SetActive(false);
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

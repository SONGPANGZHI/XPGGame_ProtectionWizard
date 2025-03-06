using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoubleHitManager : MonoBehaviour
{
    public static DoubleHitManager Instance;

    public int doubleHitCount = 0;
    public int limitTime = 10;               //����ʱ������ �ݶ�10s
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

    //���������ж�
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
            DebugColorRed("��ʼ��ʱ");
            timewait = true;
            StartCoroutine(TimeFunc(limitTime));

        }
    }

    //������ʱ�� �ݶ�10s
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
        DebugColorRed("���¼�������");
    }

    //�����������
    public void ClearDoubleHitCount()
    {
        doubleHitCount = 0;
        doubleHit_UI.SetActive(false);
        DebugColorYellow("���������㣺" + doubleHitCount);
    }

    //�ۼ���������
    public int DoubleHitTimes()
    {
        doubleHitCount += 1;
        DebugColorYellow("�����������" + doubleHitCount);
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


    #region DebugLog��ɫ

    /// <summary>
    /// �����ɫLog
    /// </summary>
    public void DebugColorRed(string _log)
    {
        Debug.Log(string.Format("<color=#FF3434>{0}</color>", _log));
    }

    /// <summary>
    /// �����ɫLog
    /// </summary>
    public void DebugColorYellow(string _log)
    {
        Debug.Log(string.Format("<color=#F5FF34>{0}</color>", _log));
    }

    #endregion
}

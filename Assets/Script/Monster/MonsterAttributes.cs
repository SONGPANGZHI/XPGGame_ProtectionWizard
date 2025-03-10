using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAttributes : MonoBehaviour
{
    //怪物类别
    public enum MonterType
    {
        Goodness,
        Evil
    }

    //动画状态
    public enum MonterAnimType
    {
        Standby,
        Birth,
        Die
    }

    [SerializeField]
    private MonterType monterType;              //怪物类别
    private MonterAnimType monterAnimType;
    private bool isDie=false;
    public GameObject monterPrefab;
        
    [SerializeField]
    private Text scoreText;                     //得分文本

    [SerializeField]
    private GameObject dieEffects;              //死亡特效

    [SerializeField]
    private Animator animatorE;

    [SerializeField]
    private Animator animatorObj;

    [SerializeField]
    private GameObject warningTip;

    [SerializeField]
    private GameObject particleObj;

    [SerializeField]
    private Vector3 particePos;

    private GameObject particleTrailInstance;

    public bool Goodness2;//是否是第二关npc

    public bool Evil2;//是否是第二关其他分值小怪   20

    public bool Evil3;//是否是第二关其他分值小怪   30

    public int Evil3Int=0;

    public bool TriggerOnce = false;

    private bool dieAnim;
    
    private void OnEnable()
    {
        WarningTipShow();
        Evil3Int = Random.Range(1, 3);
    }
    private void Update()
    {
        MonsterState();
    }

    //先出预警提示
    public void WarningTipShow()
    {
        warningTip.SetActive(true);
        monterPrefab.transform.GetChild(1).gameObject.SetActive(false);
        Invoke("WarningTipEnd", 2f);
    }

    //1.5s后 预警结束开始生成怪
    public void WarningTipEnd()
    {
        Invoke("PlayDiappear", TimeToLive());
    }


    //存活时间s
    public int TimeToLive()
    {
        warningTip.SetActive(false);
        monterPrefab.transform.GetChild(1).gameObject.SetActive(true);
        int liveTime = Random.Range(5,8);
        return liveTime;
    }

    //销毁
    public void DestroyMonter()
    {
        if (!isDie)
        {
            Destroy(monterPrefab);
        }

    }

    //播放消失动画
    public void PlayDiappear()
    {
        if (!dieAnim)
        {
            animatorObj.Play("Disappear");
            Invoke("DestroyMonter", 1f);
        }
        
    }

    public void MonsterState()
    {
        switch (monterAnimType)
        {
            case MonterAnimType.Die:
                isDie = true;
                break;
        }
    }
    private void OnDisable()
    {
        DelayDestroyMonter();
    }
    public void DelayDestroyMonter()
    {
        Destroy(monterPrefab);
    }
    public void SetColli()
    {
        animatorObj.Play("Die");
    }

    //动画播放
    //3d得分UI
    //击打得分、扣分
    public void HitScore()
    {
        monterAnimType = MonterAnimType.Die;
        dieAnim = true;
        Invoke("SetColli", 1.5f);//时间可以延长点
        Invoke("DelayDestroyMonter",2f);//时间可以延长点
        Invoke("PlayAnimatorDie", 0.5f);
    }

    public GameObject moleVFX;
    public void PlayAnimatorDie()
    {
        moleVFX.SetActive(true);
        animatorE.SetTrigger("Die");
        switch (monterType)
        {
            case MonterType.Goodness:
                //Debug.Log("减20分");
                SoundManagement.Instance.PlaySFX(2);
                if (Goodness2)
                {
                    InterfaceManagement.Instance.gameUIPlane.GetComponent<GameUI>().ReduceHP();
                    GameManage.Instance.secondNpcHp -= 1;
                }
                else
                {
                    ShowFloatingText(this.transform.position, string.Format("<color=#FF3434>{0}</color>", "-20"));
                    ScoreManagement.Instance.DeductionScore(20);
                }
                break;
            case MonterType.Evil:
                //Debug.Log("加10分");
                SoundManagement.Instance.PlaySFX(1);
                if (DoubleHitManager.Instance.doubleHitScore == 0)
                {
                    if (Evil2)
                    {
                        ShowFloatingText(this.transform.position, "+20");
                        StartCoroutine(PlayTrailParticle(20));
                    }
                    else if (Evil3)
                    {
                        
                        if (Evil3Int == 1)
                        {

                            ShowFloatingText(this.transform.position, "+20");
                            StartCoroutine(PlayTrailParticle(20));
                        }
                        else if (Evil3Int == 2)
                        {
                            ShowFloatingText(this.transform.position, string.Format("<color=#FF3434>{0}</color>", "-20"));
                            ScoreManagement.Instance.DeductionScore(20);
                        }
                    }
                    else
                    {
                        ShowFloatingText(this.transform.position, "+10");
                        StartCoroutine(PlayTrailParticle(10));
                        
                    }
                }
                else
                {
                    ShowFloatingText(this.transform.position, DoubleHitManager.Instance.doubleHitScore.ToString());
                    StartCoroutine(PlayTrailParticle((int)DoubleHitManager.Instance.doubleHitScore));
                }
                break;
        }
    }

    //延迟一秒加分播放特效

    

    IEnumerator PlayTrailParticle(int _score)
    {
        StartCoroutine(InitParticleTrail());
        yield return new WaitForSeconds(1f);
        ScoreManagement.Instance.GetScore(_score);

    }

    public IEnumerator InitParticleTrail()
    {
        particleTrailInstance = Instantiate(particleObj, this.transform);

        float destroyTime = 1f; // 文字存在的时间
        float elapsedTime = 0f;

        while (elapsedTime < destroyTime)
        {
            particleTrailInstance.transform.position = Vector3.Lerp(particleTrailInstance.transform.position, particePos, Time.deltaTime * 3f);
            //particleTrailInstance.transform.localScale = Vector3.Lerp(particleTrailInstance.transform.localScale, new Vector3(0.5f,0.5f,0.5f), Time.deltaTime * 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(particleTrailInstance);
    }

    //字体界面
    public void TextPlane()
    { 
    
    }


    //加分扣分悬浮字体
    public GameObject textPrefab; // Assign your Text prefab here in the inspector
    private GameObject floatingTextInstance;

    public void ShowFloatingText(Vector3 worldPosition, string text)
    {
        // 实例化Text Prefab
        floatingTextInstance = Instantiate(textPrefab, worldPosition + new Vector3(0, 1f, 0), Quaternion.identity,this.gameObject.transform);
        //floatingTextInstance = Instantiate(textPrefab, this.gameObject.transform);
        Text textComponent = floatingTextInstance.GetComponentInChildren<Text>();
        if (textComponent != null)
        {
            textComponent.text = text;
            StartCoroutine(MoveAndDestroy(floatingTextInstance));
        }
    }

    private IEnumerator MoveAndDestroy(GameObject textGO)
    {
        float destroyTime = 1f; // 文字存在的时间
        Vector3 targetPosition = textGO.transform.position + new Vector3(0, 2f, 0); // 向上移动的距离
        Vector3 targetScale = new Vector3(1.5f,1.5f,1.5f);
        float elapsedTime = 0f;

        while (elapsedTime < destroyTime)
        {
            textGO.transform.position = Vector3.Lerp(textGO.transform.position, targetPosition, Time.deltaTime * 2);
            textGO.transform.GetChild(0).localScale = Vector3.Lerp(textGO.transform.GetChild(0).localScale, targetScale, Time.deltaTime * 2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(textGO);
    }

}

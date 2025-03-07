using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 5.0f; // 移动速度
    public GameObject player;//主角
    private bool canMove=true;
    public bool canAttack = true;


    public CameraShake cameraShake;//摄像机抖动
    public float shakeDurationP = 0.3f;
    public float shakeMagnitudeP = 0.7f;

    public GameObject attackCube;
    private float deactivateTriggerTime;
    [SerializeField]
    private bool isProcessing = false; // 新增：用于判断是否正在处理碰撞
    public float attackTime=1f;//攻击用时

    //public int TotalScore;//总分
    //public int addScoer;//总的加的分数
    //public int subtractScoer;//总的扣的分数

    public Animator animatorP;

    public bool InceptionSet=false;//初始设置

    public Rigidbody rb;

    void Update()
    {
        //这两开始需要条件

        //KeepTime();//计时
        //Score();//得分
        //UpdateCounterText();

        if (canMove)
        {
            // 检查玩家是否有水平或垂直方向的输入
            bool hasInput = Mathf.Abs(Input.GetAxis("Horizontal")) > 0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f;
            //player.GetComponent<Animator>().SetTrigger("Run");
            if (!hasInput)
                rb.velocity = Vector3.zero;
            else
                CharacterControl();

        }

        if (Input.GetMouseButtonDown(0)&& canAttack) // 检测鼠标左键是否被按下
        {
            Attack();
        }

        if (attackCube.activeSelf==true && Time.time >= deactivateTriggerTime)
        {
            attackCube.SetActive(false);
            canMove = true;
            canAttack = true;
            isProcessing = false;
        }
    }

    //加减时间交互
    public void AddTime(float additionalTime)
    {
        GameManage.Instance.AddT(additionalTime);
    }


    //人物控制
    public void CharacterControl()
    {
        //移动
        float moveHorizontal = Input.GetAxis("Horizontal"); // 获取水平输入(A/D)
        float moveVertical = Input.GetAxis("Vertical"); // 获取垂直输入(W/S)
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); // 创建移动向量
        // 根据输入更新位置
        player.transform.Translate(movement * speed * Time.deltaTime);

        //旋转锁定
        // 获取当前物体的旋转
        Quaternion currentRotation = transform.rotation;
        // 重新计算旋转，使得物体的Z轴朝向世界坐标系的Z轴
        // 这里我们仅保留了绕Z轴的旋转部分，清除了绕X和Y轴的旋转
        Vector3 eulerRotation = currentRotation.eulerAngles;
        player.transform.rotation = Quaternion.Euler(0, 0, eulerRotation.z);
    }
    //人物攻击
    public void Attack()
    {
        //攻击相关------攻击时不能移动
        //1、动画
        //2、触发器
        //其他


        //镜头抖动
        Invoke("Shake", 0.5f);
        
        animatorP.SetTrigger("Attack");
        if (attackCube.activeSelf==false)
        {
            attackCube.SetActive(true);
            canMove = false;
            canAttack = false;
            SoundManagement.Instance.PlaySFX(0);
            deactivateTriggerTime = Time.time + attackTime;
        }
    }

    public void Shake()
    {
        cameraShake.TriggerShake(shakeDurationP, shakeMagnitudeP);//震动持续时间、大小
    }
    private void OnTriggerEnter(Collider other)
    {
        // 忽略与自己(Player)的碰撞
        if (other.gameObject.tag != "Goodness"&&other.gameObject.tag!="Evil") return;

        if (isProcessing) return;

        switch (other.gameObject.tag)
        {
            case "Goodness":
                //HIT方法、扣分
                if (!other.transform.parent.GetComponent<MonsterAttributes>().TriggerOnce)
                {
                    other.transform.parent.GetComponent<MonsterAttributes>().TriggerOnce = true;
                    other.transform.parent.GetComponent<MonsterAttributes>().HitScore();
                    if (!other.transform.parent.GetComponent<MonsterAttributes>().Goodness2)
                    {
                        ScoreManagement.Instance.DeductionScore(20);
                    }
                    DoubleHitManager.Instance.ClearDoubleHitCount();
                    isProcessing = true;
                }
                Debug.Log("打到友方");
                break;
            case "Evil":
                //hit方法、加分
                if (!other.transform.parent.GetComponent<MonsterAttributes>().TriggerOnce)
                {
                    other.transform.parent.GetComponent<MonsterAttributes>().TriggerOnce = true;
                    other.transform.parent.GetComponent<MonsterAttributes>().HitScore();
                    //DoubleHitManager.Instance.DoubleHitTimes();
                    if (DoubleHitManager.Instance.DoubleHitTimes() > 3)
                    {
                        if (other.transform.parent.GetComponent<MonsterAttributes>().Evil2)
                        {
                            DoubleHitManager.Instance.JudgeDoubleHit(20);
                        }
                        else if (other.transform.parent.GetComponent<MonsterAttributes>().Evil3)
                        {
                            int a = other.transform.parent.GetComponent<MonsterAttributes>().Evil3Int;
                            if (a==1)
                            {
                                DoubleHitManager.Instance.JudgeDoubleHit(20);
                            }

                        }
                        else
                        {
                            DoubleHitManager.Instance.JudgeDoubleHit(10);
                        }
                        Debug.LogError("开始计算连击");
                    }
                    else
                    {
                        if (other.transform.parent.GetComponent<MonsterAttributes>().Evil2)
                        {
                            ScoreManagement.Instance.GetScore(20);
                        }
                        else if (other.transform.parent.GetComponent<MonsterAttributes>().Evil3)
                        {
                            int a = other.transform.parent.GetComponent<MonsterAttributes>().Evil3Int;
                            if (a == 1)
                            {
                                ScoreManagement.Instance.GetScore(20);
                            }
                            else if (a == 2)
                            {
                                ScoreManagement.Instance.DeductionScore(20);
                                DoubleHitManager.Instance.ClearDoubleHitCount();
                            }
                        }
                        else
                        {
                            ScoreManagement.Instance.GetScore(10);
                        }
                    }
                    GameManage.Instance.currentCount += 1;
                    isProcessing = true;
                }
                Debug.Log("打到敌方");
                break;
        }
    }

}

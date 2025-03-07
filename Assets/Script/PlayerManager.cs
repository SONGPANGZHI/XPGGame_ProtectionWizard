using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 5.0f; // �ƶ��ٶ�
    public GameObject player;//����
    private bool canMove=true;
    public bool canAttack = true;


    public CameraShake cameraShake;//���������
    public float shakeDurationP = 0.3f;
    public float shakeMagnitudeP = 0.7f;

    public GameObject attackCube;
    private float deactivateTriggerTime;
    [SerializeField]
    private bool isProcessing = false; // �����������ж��Ƿ����ڴ�����ײ
    public float attackTime=1f;//������ʱ

    //public int TotalScore;//�ܷ�
    //public int addScoer;//�ܵļӵķ���
    //public int subtractScoer;//�ܵĿ۵ķ���

    public Animator animatorP;

    public bool InceptionSet=false;//��ʼ����

    public Rigidbody rb;

    void Update()
    {
        //������ʼ��Ҫ����

        //KeepTime();//��ʱ
        //Score();//�÷�
        //UpdateCounterText();

        if (canMove)
        {
            // �������Ƿ���ˮƽ��ֱ���������
            bool hasInput = Mathf.Abs(Input.GetAxis("Horizontal")) > 0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f;
            //player.GetComponent<Animator>().SetTrigger("Run");
            if (!hasInput)
                rb.velocity = Vector3.zero;
            else
                CharacterControl();

        }

        if (Input.GetMouseButtonDown(0)&& canAttack) // ����������Ƿ񱻰���
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

    //�Ӽ�ʱ�佻��
    public void AddTime(float additionalTime)
    {
        GameManage.Instance.AddT(additionalTime);
    }


    //�������
    public void CharacterControl()
    {
        //�ƶ�
        float moveHorizontal = Input.GetAxis("Horizontal"); // ��ȡˮƽ����(A/D)
        float moveVertical = Input.GetAxis("Vertical"); // ��ȡ��ֱ����(W/S)
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); // �����ƶ�����
        // �����������λ��
        player.transform.Translate(movement * speed * Time.deltaTime);

        //��ת����
        // ��ȡ��ǰ�������ת
        Quaternion currentRotation = transform.rotation;
        // ���¼�����ת��ʹ�������Z�ᳯ����������ϵ��Z��
        // �������ǽ���������Z�����ת���֣��������X��Y�����ת
        Vector3 eulerRotation = currentRotation.eulerAngles;
        player.transform.rotation = Quaternion.Euler(0, 0, eulerRotation.z);
    }
    //���﹥��
    public void Attack()
    {
        //�������------����ʱ�����ƶ�
        //1������
        //2��������
        //����


        //��ͷ����
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
        cameraShake.TriggerShake(shakeDurationP, shakeMagnitudeP);//�𶯳���ʱ�䡢��С
    }
    private void OnTriggerEnter(Collider other)
    {
        // �������Լ�(Player)����ײ
        if (other.gameObject.tag != "Goodness"&&other.gameObject.tag!="Evil") return;

        if (isProcessing) return;

        switch (other.gameObject.tag)
        {
            case "Goodness":
                //HIT�������۷�
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
                Debug.Log("���ѷ�");
                break;
            case "Evil":
                //hit�������ӷ�
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
                        Debug.LogError("��ʼ��������");
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
                Debug.Log("�򵽵з�");
                break;
        }
    }

}

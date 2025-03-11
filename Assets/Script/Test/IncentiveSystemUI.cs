
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IncentiveSystemUI : Singleton<IncentiveSystemUI>
{

    [SerializeField] private GameObject _gameObject;
    private Camera _camera;
    public GameObject planeObject;
    public float enableTime;
    public Text t;
    void Start()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        _gameObject.transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
        if (Time.time> enableTime)
        {
            planeObject.SetActive(false);
        }
        MonitorOther();
    }
    private bool hitOne = false;
    private bool hitTwo = false;
    private bool hitThree = false;
    private bool scoreOne = false;
    private bool scoreTwo = false;
    public void MonitorOther()
    {
        if (DoubleHitManager.Instance.doubleHitCount == 0)
        {
            hitOne = true;
            hitTwo = true;
            hitThree = true;
        }
        if (DoubleHitManager.Instance.doubleHitCount == 1&& hitOne)//第一次击中
        {
            Debug.Log("hitOne");
            int index = UnityEngine.Random.Range(0, 2);
            Export_EncouragingContentEn(index);
            hitOne = false;
        }
        else if (DoubleHitManager.Instance.doubleHitCount == 3 && hitTwo)//连续3次
        {
            int index = UnityEngine.Random.Range(2, 4);
            Export_EncouragingContentEn(index);
            hitTwo = false;
        }
        else if (DoubleHitManager.Instance.doubleHitCount == 5 && hitThree)//连续5次
        {
            Export_EncouragingContentEn(4);
            hitThree = false;
        }

        if (ScoreManagement.Instance.TotalScore==0)
        {
            scoreOne = true;
            scoreTwo = true;
        }
        if (ScoreManagement.Instance.TotalScore >=100 && scoreOne)
        {
            Export_EncouragingContentEn(7);
            scoreOne = false;
        }
        else if (ScoreManagement.Instance.TotalScore >= 200 && scoreTwo)
        {
            Export_EncouragingContentEn(8);
            scoreTwo = false;
        }

    }
    public void Set_enableTime()
    {
        enableTime = Time.time + 2;
    }
    

    [SerializeField]
    private List<IncentiveSystemString> incentiveSS = new List<IncentiveSystemString>();
    public string I_content;
    //public AudioSource Speak;
    //public AudioClip I_contentAudio;
    //根据
    public void Get_EncouragingContent(int index)
    {
        var foundIndex= incentiveSS.FirstOrDefault(person => person.index == index);
        I_content = foundIndex.content;
        //I_contentAudio = foundIndex.contentAudio;
    }
    public void Set_EncouragingContent()
    {
        Set_enableTime();
        planeObject.SetActive(true);
        t.text = I_content;
        //Speak.clip = I_contentAudio;
        //Speak.Play();
    }
    public void Export_EncouragingContentEn(int index)
    {
        var foundIndex = incentiveSS.FirstOrDefault(person => person.index == index);
        I_content = foundIndex.content;
        //I_contentAudio = foundIndex.contentAudio;

        Set_EncouragingContent();
    }
}

[Serializable]
public class IncentiveSystemString
{
    public int index;
    public string content;
    //public AudioClip contentAudio;
}

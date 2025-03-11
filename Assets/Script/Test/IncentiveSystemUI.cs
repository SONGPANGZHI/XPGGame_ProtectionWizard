
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IncentiveSystemUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    private Camera _camera;
    public GameObject planeObject;
    private float enableTime;
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
    //ИљОн
    public void Get_EncouragingContent(int index)
    {
        var foundIndex= incentiveSS.FirstOrDefault(person => person.index == index);
        I_content = foundIndex.content;
        //I_contentAudio = foundIndex.contentAudio;
    }
    public void Set_EncouragingContent()
    {
        planeObject.SetActive(true);
        Set_enableTime();
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

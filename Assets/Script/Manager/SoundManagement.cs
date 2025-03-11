using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public static SoundManagement Instance;

    public enum AudioSourceType
    {
        hitSFX = 1,             //击打音效
        scoreSFX = 2,           //得分音效
        deductionSFX = 3,       //减分音效
        pickUpPropsSFX = 4,     //拾取道具
        endSFX = 5,             //结束界面音效
    }

    public AudioSource BGM;
    public AudioSource SFX;
    public List<AudioClip> SFXList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    //播放BGM
    public void PlayBGM()
    {
        BGM.Play();
    }

    //播放音效
    public void PlaySFX(int ID)
    {
        SFX.clip = SFXList[ID];
        SFX.Play();
    }
}

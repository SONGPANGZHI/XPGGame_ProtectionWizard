using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public static SoundManagement Instance;

    public enum AudioSourceType
    {
        hitSFX = 1,             //������Ч
        scoreSFX = 2,           //�÷���Ч
        deductionSFX = 3,       //������Ч
        pickUpPropsSFX = 4,     //ʰȡ����
        endSFX = 5,             //����������Ч
    }

    public AudioSource BGM;
    public AudioSource SFX;
    public List<AudioClip> SFXList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    //����BGM
    public void PlayBGM()
    {
        BGM.Play();
    }

    //������Ч
    public void PlaySFX(int ID)
    {
        SFX.clip = SFXList[ID];
        SFX.Play();
    }
}

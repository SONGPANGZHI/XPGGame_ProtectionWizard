using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrailManagement : MonoBehaviour
{
    public static ParticleTrailManagement instance;

    [SerializeField]
    private Transform targetPos;            //Ŀ���
    [SerializeField]
    private GameObject targetParticle;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

   
}

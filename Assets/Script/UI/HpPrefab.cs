using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPrefab : MonoBehaviour
{
    //�ص�����
    public void CloseHPRed()
    { 
        transform.GetChild(0).gameObject.SetActive(false);
    }
}

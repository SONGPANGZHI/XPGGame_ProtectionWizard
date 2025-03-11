using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPrefab : MonoBehaviour
{
    //关掉红心
    public void CloseHPRed()
    { 
        transform.GetChild(0).gameObject.SetActive(false);
    }
}

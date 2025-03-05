using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public GameObject gameObjectVFX;

    public void SetVFX()
    {
        gameObjectVFX.SetActive(true);
    }
    public void DisVFX()
    {
        gameObjectVFX.SetActive(false);
    }
}

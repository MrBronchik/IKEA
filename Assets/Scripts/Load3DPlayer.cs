using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Load3DPlayer : MonoBehaviour
{
    public void LoadThisInstr()
    {
        PlayerPrefs.SetString("InstrDir", this.transform.GetChild(0).name);
        SceneManager.LoadScene("3D_Player");
    }
}

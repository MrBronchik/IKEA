using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    
    public void btnChangeScene(string scene_name)
    {
        Debug.Log("Pre-check");
        
        SceneManager.LoadScene(scene_name);

        Debug.Log("It works!");
    }

}

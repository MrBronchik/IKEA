using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


public class Player : MonoBehaviour
{
    string dirPath;

    private void Awake() {
        dirPath = PlayerPrefs.GetString("InstrDir");
    }
    
    
    private void Start() {
        Debug.Log(dirPath);

        UnityEngine.Object prefabGO = Resources.Load(@"Assets\database\002\anim.prefab");
        Instantiate(prefabGO, new Vector3(0, 0, 0), Quaternion.identity);
    }

    /*private Animation anim;
    float tickInTotal; //A number of ticks in total
    int stepCounter; //A number of steps, which this animation have

    // ----- TO DELETE -----
    private string[] stepsTickStr = {"0", "20"};
    // -----           -----
    //List<string> stepsTickStr = File.ReadAllLines(@"E:\IKEA\New Unity Project\Assets\Testing\New Folder\first anim\Steps.txt").ToList(); //Taking from Steps.txt file

    List<float> stepsTick = new List<float>();
    public string animName; //Animation name (blender's deafault = "Scene")
    public GameObject InGameObject;
    public GameObject SpeedMenuBut;
    public GameObject SpeedMenuCanvas;

    void Start()
    {
        anim = InGameObject.GetComponent<Animation>();

        SetAnimSpeed(1);
        anim.Stop();

        tickInTotal = anim[animName].length * 24f + 1f;
        //Debug.Log ("Tickframes = " + tickInTotal);

        foreach(string numstr in stepsTickStr)
        {
            stepsTick.Add(float.Parse(numstr));
        }
        stepsTick.Add(tickInTotal);

        stepCounter = 1;
    }

    void Update()
    {
        if (anim.IsPlaying(animName) == true) {
            if (anim[animName].time*24 >= stepsTick[stepCounter]-1) {
                anim.Stop();
            }
        }
    }

    public void SetAnimSpeed(float speed)
    {
        foreach (AnimationState state in anim)
        {
            state.speed = speed; //1F deafault
        }
    }

    public void ReAnimButClick() {

        if (anim.IsPlaying(animName) == true) {

            anim.Stop();

        } else {

            anim[animName].time = stepsTick[stepCounter-1]/24f;
            anim.Play(animName);

        }
    }

    public void ChangeStep(int pushStepby) {
        if (stepCounter != 1 && pushStepby == -1){
            stepCounter--;
        }
        if (stepCounter != stepsTick.Count-1 && pushStepby == 1){
            stepCounter++;
        }
    }*/
}
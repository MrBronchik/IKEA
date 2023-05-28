using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
 
public class Control : MonoBehaviour
{

    public Camera cam;
    public float sens = 0.01F;

    public float varianceInDistances = 200.0F;
    public float step = -1000.0F;
    public float zoomPerc = 0F;
    public float camSpeed = 100;
    private float lastFieldOfView = 90F;

    private Vector2 touchDelta = new Vector2(0,0);
    private Vector2 firstDist = new Vector2(0,0);
    private Vector2 SecondDist = new Vector2(0,0);

    private bool hasTouched = false;
    private bool isInZone = true;
    private bool startPointSet = false;

    [SerializeField] Button btnUp;
    [SerializeField] Button btnRight;
    [SerializeField] Button btnDown;
    [SerializeField] Button btnLeft;

    public int updownCoef;
    public int rightleftCoef;

    
    void Update()
    {
        if (Input.touchCount == 1) this.LookRotation(cam.transform);
        if (Input.touchCount == 2) this.Zoom(cam);

        if (hasTouched == true && Input.touchCount != 2) {
            hasTouched = false;
            lastFieldOfView = cam.fieldOfView;
            isInZone = true;
            startPointSet = false;
        }

        MoveCam(updownCoef, rightleftCoef);
    }

    public void LookRotation(Transform camera)                  //Change the x and y rotation of camera
    {
        float yRot = 0;
        float xRot = 0;
        
        yRot =  Input.touches[0].deltaPosition.x * sens;               //get x and y of touches of screen
        xRot = -Input.touches[0].deltaPosition.y * sens;

        camera.localRotation *= Quaternion.Euler(0f, yRot, 0f);        //To change camera's rotation around y-axis
        camera.localRotation *= Quaternion.Euler(xRot, 0f, 0f);        //To change camera's rotation around x-axis

        camera.localRotation = Quaternion.Euler(camera.transform.localEulerAngles.x, camera.transform.localEulerAngles.y, 0f);  //set z rotation to 0
    }

    public void Zoom(Camera camera) {
        
        if (hasTouched == false) {
            hasTouched = true;
            firstDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
            touchDelta.x = 0F;
            touchDelta.y = 0F;
        }

        touchDelta = Input.GetTouch(0).position - Input.GetTouch(1).position;

        if ((touchDelta.magnitude - firstDist.magnitude > varianceInDistances || touchDelta.magnitude - firstDist.magnitude < -varianceInDistances) && isInZone == true){
            
            SecondDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
            isInZone = false;
            startPointSet = true;
        }

        if (!isInZone) {
            zoomPerc = (touchDelta.magnitude - SecondDist.magnitude) / step;
            camera.fieldOfView = Mathf.Clamp(lastFieldOfView + 90 * zoomPerc,15,90);
        }
    }

    public void MoveCam(int _updownCoef, int _rightleftCoef)
    {
        switch (_updownCoef) {
            case -1:
                MoveCamForward();
                break;

            case 1:
                MoveCamBackward();
                break;
        }

        switch (_rightleftCoef) {
            case -1:
                MoveCamRightward();
                break;

            case 1:
                MoveCamLeftward();
                break;
        }
    }

    public void ChangeDownUpCoef(int _int)
    {
        updownCoef += _int;
    }

    public void ChangeRightLeftCoef(int _int)
    {
        rightleftCoef += _int;
    }

    private void MoveCamForward()
    {
        cam.transform.Translate(Vector3.forward * Time.deltaTime * camSpeed);
    }

    private void MoveCamRightward()
    {
        cam.transform.Translate(Vector3.right * Time.deltaTime * camSpeed);
    }

    private void MoveCamBackward()
    {
        cam.transform.Translate(Vector3.back * Time.deltaTime * camSpeed);
    }

    private void MoveCamLeftward()
    {
        cam.transform.Translate(Vector3.left * Time.deltaTime * camSpeed);
    }
}
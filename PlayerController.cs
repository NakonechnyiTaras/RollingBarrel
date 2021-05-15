using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField] FloatingJoystick _Joystick;

    public GameObject rightAxis;
    public GameObject leftAxis;

    public float mainSpeed = 0.3f;
    public float turningSpead = 0.2f;
    public float rotationLimiter = 0.2f;

    
    private float differenceLeft;
    private float differenceRight;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
     
    }

    void FixedUpdate()
    {
        GetParameters();
        Controller();

       // Debug.Log( differenceLeft);
       // Debug.Log( differenceRight);

    }

    public void GetParameters()
    {
        
        differenceLeft = rightAxis.transform.position.x - leftAxis.transform.position.x;
        differenceRight = leftAxis.transform.position.x - rightAxis.transform.position.x;

    }

    public void Controller()
    {

        rb.AddForce(mainSpeed, 0.0f, 0.0f);

        if (_Joystick.Direction.x == 0)
        {

            TurnLeveler();
        }
       else if (differenceLeft < 0.8f && Mathf.Sign(_Joystick.Direction.x) < Mathf.Epsilon)
        {

            TurnLeft();
        }
        else if (differenceRight < 0.8f)
        {

            TurnRight();
        }
        else
        {
            return;
        }
      
    }

    public void TurnLeft()
    {
        Vector3 TorqueLeft = new Vector3(0.0f, -(turningSpead / 10 * Time.deltaTime), 0.0f);
        rb.AddTorque(TorqueLeft, ForceMode.Impulse);
    }

    public void TurnRight()
    {
        Vector3 TorqueRight = new Vector3(0.0f, turningSpead / 10 * Time.deltaTime, 0.0f);
        rb.AddTorque(TorqueRight, ForceMode.Impulse);
    }

   
    private void TurnLeveler()
    {

        if (Mathf.Abs(differenceLeft) < rotationLimiter)
        {
            //Debug.Log("Case 1");
            return;
           
        }
        else if (differenceLeft > rotationLimiter)
        {
            TurnRight();
        }
        else
        {
            TurnLeft();
        }
         
    }
}



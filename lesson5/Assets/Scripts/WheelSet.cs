using UnityEngine;
using System.Collections;
public enum Side
    {
        left,
        right
    };

    [System.Serializable]
public class WheelSet  {

    Quaternion angleCorrection = Quaternion.Euler(0.0f, 0.0f, 90.0f); 
    const int dps = 6; 

    public GameObject wheelLeftGo;
    public Transform wheelLeftT;
    WheelCollider wheelLeftWc;

    public GameObject wheelRightGo;
    public Transform wheelRightT;
    WheelCollider wheelRightWc;

    public bool drive; 
    public bool brakes; 
    public bool steering; 

    float steerAngle;
    Vector3 wheelRightRotationCurrent;
    Vector3 wheelLeftRotationCurrent;
    Vector3 tempRotation = new Vector3(0.0f, 0.0f, 0.0f);

   
   
    public void Throttle(Side side, float motor, float max)
    {
        if (drive)
        {
            if (side == Side.left)
                wheelLeftWc.motorTorque = -1 * max * motor;
            else
                wheelRightWc.motorTorque = -1 * max * motor;
        }
    }

   
    public void Brake(Side side, float brake, float max)
    {
        if (brakes)
        {
            if (side == Side.left)
                wheelLeftWc.brakeTorque = max * brake;
            else
                wheelRightWc.brakeTorque = max * brake;
        }
    }

    public void Steer(Side side, float amount, float max)
    {
        steerAngle = amount * max;

        if (steering)
        {
            if (side == Side.left)
                wheelLeftWc.steerAngle = steerAngle;
            else
                wheelRightWc.steerAngle = steerAngle;
        }
    }

   
    public void Init()
    {
        wheelLeftWc = wheelLeftT.GetComponent<WheelCollider>();
        wheelRightWc = wheelRightT.GetComponent<WheelCollider>();

        wheelLeftRotationCurrent = wheelLeftGo.transform.eulerAngles;
        wheelRightRotationCurrent = wheelLeftGo.transform.eulerAngles;
    }

  
    public void UpdateWheels()
    {
       
        wheelLeftGo.transform.position = wheelLeftWc.transform.position;
        wheelRightGo.transform.position = wheelRightWc.transform.position;

      
        if (steering)
        {
            tempRotation.y = Mathf.Lerp(tempRotation.y, steerAngle, 0.2f);
            wheelRightRotationCurrent.y = tempRotation.y;
            wheelLeftRotationCurrent.y = tempRotation.y;
            wheelLeftGo.transform.localEulerAngles = wheelLeftRotationCurrent;
            wheelRightGo.transform.localEulerAngles = wheelRightRotationCurrent;
        }
        else
        {
            wheelLeftGo.transform.rotation = wheelLeftWc.transform.rotation * angleCorrection;
            wheelRightGo.transform.rotation = wheelRightWc.transform.rotation * angleCorrection;
        }

       
        wheelLeftGo.transform.Rotate(0.0f, wheelLeftWc.rpm * dps * Time.deltaTime, 0.0f);
        wheelRightGo.transform.Rotate(0.0f, wheelRightWc.rpm * dps * Time.deltaTime, 0.0f);
    }

   
    public string GetTorque(Side side)
    {
        if (side == Side.left)
            return wheelLeftWc.motorTorque.ToString();
        else
            return wheelRightWc.motorTorque.ToString();
    }
}

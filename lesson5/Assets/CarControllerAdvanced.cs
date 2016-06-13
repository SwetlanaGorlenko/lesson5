using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CarControllerAdvanced : MonoBehaviour
{
    public WheelSet frontSet;
    public WheelSet backSet;

    public float steerMax; 
    public float motorMax; 
    public float brakeMax; 

    public Vector3 objectCentreOfMass; 

    public bool debugDisplay = true; 

    float steer = 0.0f;
    float forward = 0.0f;
    float back = 0.0f;
    float speed = 0.0f; 
    float motor = 0.0f;
    float brake = 0.0f;
    public int playerHealth = 100;

    bool reverse = false;
    
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass += objectCentreOfMass;

      
        frontSet.Init();
        backSet.Init();

    }
    void OnGUI()
    {
     
        if (debugDisplay)
        {
            GUI.Label(new Rect(10.0f, 50.0f, 500.0f, 20.0f), "playerHealt: " + playerHealth.ToString());
        }
    }
   
    void Update()
    {
        steer = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);
        forward = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
        back = -1 * Mathf.Clamp(Input.GetAxis("Vertical"), -1, 0);

        frontSet.UpdateWheels();
        backSet.UpdateWheels();

    
        speed = GetComponent<Rigidbody>().velocity.magnitude;

        if ((int)speed == 0)
        { 
            if (back > 0)
                reverse = true;
            if (forward > 0)
                reverse = false;
        }

        if (reverse)
        {
            motor = -1 * back;
            brake = forward;
        }
        else
        {
            motor = forward;
            brake = back;
        }

    }
    void FixedUpdate()
    {

     
        frontSet.Throttle(Side.left, motor, motorMax);
        frontSet.Throttle(Side.right, motor, motorMax);
        backSet.Throttle(Side.left, motor, motorMax);
        backSet.Throttle(Side.right, motor, motorMax);

     
        frontSet.Brake(Side.left, brake, brakeMax);
        frontSet.Brake(Side.right, brake, brakeMax);
        backSet.Brake(Side.left, brake, brakeMax);
        backSet.Brake(Side.right, brake, brakeMax);

      
        frontSet.Steer(Side.left, steer, steerMax);
        frontSet.Steer(Side.right, steer, steerMax);
        backSet.Steer(Side.left, steer, steerMax);
        backSet.Steer(Side.right, steer, steerMax);
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody && collision.gameObject.tag == "Priz")
        {
            playerHealth += 10;

        }
      
    }
    void OnTriggerEnter(Collider other)
    {         
         playerHealth -= 10;
    }
}
using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.rigidbody)
        {
            Destroy(gameObject);

        }
       
         
    }
}

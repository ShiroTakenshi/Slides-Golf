using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGravity : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float gravityMagnitude;

    bool useGyro;
    Vector3 gravityDir;
    void Start()
    {
        if(SystemInfo.supportsGyroscope)
        {
            useGyro = true;
            Input.gyro.enabled = true;
        }
    }

    //update is called once per frame
    void Update()
    {
        var inputDir = useGyro ? Input.gyro.gravity : Input.acceleration;
         gravityDir = new Vector3
        (
            //atur lagi arah karena orientasi kamera berbeda dengan orientasi world game.
            inputDir.x,
            inputDir.z,
            inputDir.y
        );
    }
    private void FixedUpdate() {
        //menggunakan constant acceleration karena gravity  merupakan acceleration.
        rb.AddForce( gravityDir * gravityMagnitude, ForceMode.Acceleration);    
    }
}

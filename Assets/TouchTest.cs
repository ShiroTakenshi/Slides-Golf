using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchTest : MonoBehaviour
{
    // public void Start()
    // {
    //     Debug.Log(SystemInfo.supportsGyroscope);
    //     Debug.Log(SystemInfo.supportsAccelerometer);

    // }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);

        //swipe
        if (touch.deltaPosition.x > 10)
            Debug.Log("Right"); 
        else if (touch.deltaPosition.x < -10)
            Debug.Log("Left");

        //tap
        if (touch.tapCount > 0)
            Debug.Log(touch.tapCount);

    }

    private void OnDrawGizmos()
    {
        foreach (var touch in Input.touches)
        {
            var TouchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
            TouchWorldPos.z = 0;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Gizmos.color = Color.blue;
                    break;
                case TouchPhase.Stationary:
                    Gizmos.color = Color.red;
                    break;
                case TouchPhase.Moved:
                    Gizmos.color = Color.green;
                    break;
                case TouchPhase.Ended:
                    Gizmos.color = Color.yellow;
                    break;
                case TouchPhase.Canceled:
                    Gizmos.color = Color.magenta;
                    break;
                default:
                    break;
            }
            Gizmos.DrawSphere(TouchWorldPos, 0.5f);
        }
    }

}

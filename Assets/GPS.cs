using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{
    [SerializeField] string latitude;
    [SerializeField] string longitude;
    // [SerializeField] string altitude;
    // [SerializeField] string horizontalAccuracy;
    // [SerializeField] string timestamp;

    Coroutine ActiveGPSCroutine;
    void Update()
    {
        // Time.timeScale = 0.5f;
        // Time.deltaTime
        // Time.unscaledDeltaTime;

        if (Input.location.status == LocationServiceStatus.Running)
            Debug.Log("Run");

        // latitude = "xxx." + Input.location.lastData.latitude.ToString("F4").Split('.')[1];
        // longitude ="xxx." +  Input.location.lastData.longitude.ToString("F4").Split('.')[1];
        // altitude = "xxx." + Input.location.lastData.altitude.ToString("F4").Split('.')[1];
        // horizontalAccuracy = Input.location.lastData.horizontalAccuracy.ToString();
        // timestamp = Input.location.lastData.timestamp.ToString();
    }

    private void OnEnable()
    {
        if (ActiveGPSCroutine == null)
            ActiveGPSCroutine = StartCoroutine(ActiveGPS());
    }

    private void OnDisable()
    {
        StopCoroutine(ActiveGPSCroutine);

        if (Input.location.status == LocationServiceStatus.Running)
            Input.location.Stop();
    }


    IEnumerator ActiveGPS()
    {
        Debug.Log("Unity Remote Connecting");
        while (UnityEditor.EditorApplication.isRemoteConnected == false)
            yield return new WaitForSecondsRealtime(1);

        Debug.Log("UNITY Remote Connected");

        if (Input.location.isEnabledByUser == false)
        {
            Debug.Log("Location service is not enabled by user");
            yield break;
        }
        Debug.Log("Start Location Services");
        Input.location.Start();

        int maxWait = 15;
        while ((Input.location.status == LocationServiceStatus.Stopped
            || Input.location.status == LocationServiceStatus.Initializing)
            || maxWait > 0)
        {
            Debug.Log("Location service status check: " + Input.location.status);
            yield return new WaitForSecondsRealtime(1);
            maxWait -= 1;
        }

        if (maxWait < 1)
        {
            Debug.Log("Location service failed: Time Out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Location Service failed to start");
            yield break;
        }

    }
}

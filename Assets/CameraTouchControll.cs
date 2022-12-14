using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchControll : MonoBehaviour
{
    [SerializeField, Range(0, 20)] float filterFactor = 1;
    [SerializeField, Range(0, 10)] float dragFactor = 1;
    [SerializeField, Range(0, 2)] float zoomFactor = 1;
    [Tooltip("equal camera y position")]
    [SerializeField] float minCamPos = 70;
    [SerializeField] float maxCamPos = 170;
    // [SerializeField] float minX;
    // [SerializeField] float maxX;
    // [SerializeField] float minZ;
    // [SerializeField] float maxZ;
    [SerializeField] Collider topCollider;


    float distance;
    void Start()
    {
        distance = this.transform.position.y;
    }

    Vector3 touchBeganWorldPos;
    Vector3 cameraBeganWorldPos;

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        var touch0 = Input.GetTouch(0);

        //simpan posisi awal tapi posisi real world
        if (touch0.phase == TouchPhase.Began)
        {
            touchBeganWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(touch0.position.x, touch0.position.y, distance));
            cameraBeganWorldPos = this.transform.position;
        }

        if (touch0.phase == TouchPhase.Moved)
        {
            //posisi touch  (world space) saat ini.
            var touchMovedWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(touch0.position.x, touch0.position.y, distance));

            //perbedaan posisi (world space)
            var delta = touchMovedWorldPos - touchBeganWorldPos;

            //menggunakan Lerp(Linier Interpolation) sebagail filter agar
            //movement lebih smooth.
            var targetPos = cameraBeganWorldPos - delta * dragFactor;

            //TODO clamp targetPos
            targetPos = new Vector3(
                Mathf.Clamp(targetPos.x, topCollider.bounds.min.x, topCollider.bounds.max.x),
                targetPos.y,
                Mathf.Clamp(targetPos.z, topCollider.bounds.min.z, topCollider.bounds.max.z)
            );

            this.transform.position = Vector3.Lerp(
                this.transform.position,
                targetPos,
                Time.deltaTime * filterFactor
            );


        }
        if (Input.touchCount < 2)
            return;

        var touch1 = Input.GetTouch(1);

        if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
        {
            var touch0PrevPos = touch0.position - touch0.deltaPosition;
            var touch1PrevPos = touch1.position - touch1.deltaPosition;
            var prevDistance = Vector3.Distance(touch0PrevPos, touch1PrevPos);
            var currDistance = Vector3.Distance(touch0.position, touch1.position);
            var delta = currDistance - prevDistance;

            this.transform.position -= new Vector3(0, delta * zoomFactor, 0);

            //batas zoom            
            this.transform.position = new Vector3(
                this.transform.position.x,
                Mathf.Clamp(this.transform.position.y, minCamPos, maxCamPos),
                this.transform.position.z
            );
            distance = this.transform.position.y;
        }
    }
}

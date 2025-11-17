using System;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 startTouch;
    private float lastTapTime = 0f;
    private int tapCount = 0;
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
        DetectTaps();
        DetectSwipes();
        DetectPinch();
        //DetectTilt();
        //DetectGyro();
    }

    void DetectTaps()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float timeNow = Time.time;

            if (timeNow - lastTapTime < 0.3f)
                tapCount++;
            else
                tapCount = 1;

            lastTapTime = timeNow;

            if (tapCount == 1) Debug.Log("Single Tap");
            if (tapCount == 2) Debug.Log("Double Tap");
            if (tapCount == 3) Debug.Log("Triple Tap");
        }
    }

    void DetectSwipes()
    {
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
                startTouch = t.position;

            else if (t.phase == TouchPhase.Ended)
            {
                Vector2 delta = t.position - startTouch;

                if (delta.magnitude > 100)
                {
                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    {
                        if (delta.x > 0) Debug.Log("Swipe Right");
                        else Debug.Log("Swipe Left");
                    }
                    else
                    {
                        if (delta.y > 0) Debug.Log("Swipe Up");
                        else Debug.Log("Swipe Down");
                    }
                }
            }
        }
    }
    void DetectPinch()
    {
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 start0 = t0.position - t0.deltaPosition;
            Vector2 start1 = t1.position - t1.deltaPosition;

            float startDist = (start0 - start1).magnitude;
            float atualDist = (t0.position - t1.position).magnitude;

            if (atualDist > startDist + 10)
                Debug.Log("Pinch Out (Zoom In)");
            else if (atualDist < startDist - 10)
                Debug.Log("Pinch In (Zoom Out)");
        }
    }

    void DetectTilt()
    {
        Vector3 tilt = Input.acceleration;

        if (Mathf.Abs(tilt.x) > 0.2f)
            if (tilt.x > 0) Debug.Log("Tilt Right");
            else
            {
                // Debug.Log("Tilt Left");
            }

        if (Mathf.Abs(tilt.y) > 0.2f)
            if (tilt.y > 0) Debug.Log("Tilt Up");
            else
            {
                // Debug.Log("Tilt Down");
            }
        Debug.Log(tilt);
    }

    void DetectGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Vector3 rot = Input.gyro.rotationRateUnbiased;
            if (rot.sqrMagnitude > 0.01f)
                Debug.Log("Gyro rotation: " + rot);
        }
    }
}

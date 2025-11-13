using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 startTouch;
    private float lastTapTime = 0f;
    private float lastClickTime = 0f;
    private int tapCount = 0;
    int lane=3;
    Rigidbody rb;
    public float jump = 7f;
    public float constForce = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        DetectKeyboard();
        DetectSwipes();
        DetectTaps();
        //DetectPinch();
        if (lastClickTime > 0f)
        {
            lastClickTime += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * constForce, ForceMode.Force);
    }

    public void ChangeLane(bool right)
    {
        if(right && lane<5)
        {
            transform.position += new Vector3(2,0,0);
            lane++;
        }
        else if (!right && lane>1)
        {
            transform.position += new Vector3(-2,0,0);
            lane--;
        }
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

            if (tapCount == 1)
            {
                Debug.Log("Single Tap");
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }
            if (tapCount == 2) Debug.Log("Double Tap");
            if (tapCount == 3) Debug.Log("Triple Tap");
        }
    }

    void DetectKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane(false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }
    void DetectSwipes()
    {
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                startTouch = t.position;
            }

            else if (t.phase == TouchPhase.Ended)
            {
                Vector2 delta = t.position - startTouch;

                if (delta.magnitude > 100)
                {
                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    {
                        if (delta.x > 0)
                        {
                            Debug.Log("Swipe Right");
                            ChangeLane(true);
                        }
                        else
                        {
                            Debug.Log("Swipe Left");
                            ChangeLane(false);
                        }
                    }
                    //else
                    //{
                    //    if (delta.y > 0)
                    //    {
                    //        Debug.Log("Swipe Up");
                    //    }
                    //    else
                    //    {
                    //        Debug.Log("Swipe Down");
                    //    }
                    //}
                }
            }
        }
    }

    //void DetectPinch()
    //{
    //    if (Input.touchCount == 2)
    //    {
    //        Touch t0 = Input.GetTouch(0);
    //        Touch t1 = Input.GetTouch(1);

    //        Vector2 start0 = t0.position - t0.deltaPosition;
    //        Vector2 start1 = t1.position - t1.deltaPosition;

    //        float startDist = (start0 - start1).magnitude;
    //        float atualDist = (t0.position - t1.position).magnitude;


    //        if (atualDist > startDist + 10)
    //        {
    //            Debug.Log("Pinch Out (Zoom In)");
    //        }
    //        else if (atualDist < startDist - 10)
    //        {
    //            Debug.Log("Pinch In (Zoom Out)");
    //        }
    //    }
    //}
}

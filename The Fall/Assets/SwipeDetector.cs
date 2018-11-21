using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;
    private GameObject rotacion;
    public float SWIPE_THRESHOLD = 20f;
    float timecount=1.0f;
    float speedRotation = 0.05f;
    float angleRotation = 45f;
    private bool startSlerp = false;
    Quaternion rot;

    private void Start()
    {
        rotacion = GameObject.Find("Rotacion");
    }

    // Update is called once per frame

    private void rotarLeft()
    {
        if (!startSlerp)
        {
            Quaternion currentRotation = rotacion.transform.rotation;
            rot = currentRotation * Quaternion.AngleAxis(angleRotation, Vector3.up);
            startSlerp = true;
        }
    }

    private void rotarRight()
    {
        if (!startSlerp)
        {
            Quaternion currentRotation = rotacion.transform.rotation;
            rot = currentRotation * Quaternion.AngleAxis(-angleRotation, Vector3.up);
            startSlerp = true;
        }
    }

    void Update()
    {
        if (Input.GetKey("left"))
        {
            // Debug.Log("izquierda");
            //  rotacion.transform.Rotate(0, 1, 0, Space.Self);
            rotarLeft();

            // rotacion.GetComponent<Rigidbody>().AddTorque(transform.up*0.1f);
        }

        if (startSlerp)
        {
           
            rotacion.transform.rotation = Quaternion.Slerp(rotacion.transform.rotation, rot, timecount*speedRotation);
            timecount = timecount + Time.deltaTime;

            if(rotacion.transform.rotation == rot)
            {
                startSlerp = false;
            }
         
        }
        if (Input.GetKey("right"))
        {
            rotarRight();
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
        rotarLeft();
        
    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
        rotarRight();
    }
}
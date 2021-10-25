using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimStateController : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximunWalkVelocity = 0.5f;
    public float maximunRunVelocity = 2.0f;

    //  increase performance
    int VelocityZHash;
    int VelocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");

    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        //  Set current maxVelocity
        float currentMaxVelocity = runPressed ? maximunRunVelocity : maximunWalkVelocity;

        //  Handle changes in velocity
        changeVelocity(forwardPressed, leftPressed, rightPressed, backPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, backPressed, runPressed, currentMaxVelocity);

        //  Set parameters to local variable values
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }

    // handles acceleration and deceleration
    public void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backPressed, float currentMaxVelocity)
    {
        //  if player presses forward, increase velocity in Z direction
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        //  increase velocity in left direction
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        //  increase velocity in right direction
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        //  increase velocity in the back direction
        if (backPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        //  decrease velocityZ
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        //  increase velocityX if left is not pressed and velocityX < 0
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        //  decrease velocityX if right is not pressed and velocityX < 0
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        if (!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
    }

    //  handles reset and locking of velocity
    public void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backPressed, bool runPressed, float currentMaxVelocity)
    {
        //  reset velocityZ
        //if (!forwardPressed && velocityZ < 0.5f)
        //{
        //    velocityZ = 0.0f;
        //}

        // if left or right is not pressed and velocityX does not equal zero
        // & velocityX is in the range of -0.05 and 0.05
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
        //--------------------------------------forward-----------------------------//
        //lock forward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
            {
                velocityZ = currentMaxVelocity;
                //  decelerate to the mximum walk velocity
            }
            else if (forwardPressed && velocityZ > currentMaxVelocity)
            {
                velocityZ -= Time.deltaTime * deceleration;
                //  round to currentMaxVelocity if within offset
                if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
                {
                    velocityZ = currentMaxVelocity;
                }
                //  round to the currentMaxVelocity if within offset
            }
            else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        //----------------------------------end forward-----------------------------//
        //--------------------------------------left--------------------------------//
        //  lock left
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
            //  decelerate to the mximum walk velocity
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            //  round to currentMaxVelocity if within offset
            if (velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
            //  round to the currentMaxVelocity if within offset
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }
        //----------------------------------end left--------------------------------//
        //--------------------------------------right-------------------------------//
        //  lock right
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
            //  decelerate to the mximum walk velocity
        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            //  round to currentMaxVelocity if within offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
            //  round to the currentMaxVelocity if within offset
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
        //----------------------------------end right-------------------------------//
        //--------------------------------------back--------------------------------//
        //  lock back
        if (backPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
            //  decelerate to the maximum walk velocity
        }
        else if (backPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            //  round to currentMaxVelocity if within offset
            if (velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
            {
                velocityZ = -currentMaxVelocity;
            }
            //  round to the currentMaxVelocity if within offset
        }
        else if (backPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }
        //----------------------------------end back--------------------------------//
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");

        if ((forwardPressed || leftPressed || rightPressed || backPressed) && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if ((!forwardPressed || !leftPressed || !rightPressed || !backPressed) && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        //if (!isRunning && (forwardPressed || leftPressed || rightPressed || backPressed && runPressed))
        //{
        //    animator.SetBool(isRunningHash, true);
        //}

        //if (isRunning && (!forwardPressed || !leftPressed || !rightPressed || !backPressed && !runPressed))
        //{
        //    animator.SetBool(isRunningHash, false);
        //}
    }
}

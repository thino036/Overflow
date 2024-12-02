using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateControl : MonoBehaviour
{
    public PlayerCube player;
    public Goal goal;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey("a") || Input.GetKey("d")) && player.isAlive && !goal.gameover)
        {
            animator.SetBool("IsRunning", true);
        }

        if (!Input.GetKey("a") && !Input.GetKey("d"))
        {
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetKey("w") && player.isAlive && !goal.gameover)
        {
            animator.SetBool("IsJumping", true);
        }

        if (!Input.GetKey("w"))
        {
            animator.SetBool("IsJumping", false);
        }
    }
}

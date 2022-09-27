using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayerAnimation : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayRunAnimation()
    {
        animator.SetBool("isRunning", true);
    }

    public void PlayIdleAnimation()
    {
        animator.SetBool("isRunning", false);
    }
}

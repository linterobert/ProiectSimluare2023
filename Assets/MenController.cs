using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MenController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }
}

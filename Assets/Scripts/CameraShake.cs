using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private Animator animator;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CameraShaking()
    {
        animator.SetTrigger("Shake");
    }
}

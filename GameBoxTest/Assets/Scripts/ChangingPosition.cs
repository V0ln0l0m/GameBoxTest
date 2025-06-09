using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingPosition : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangPosition()
    {
        anim.SetBool("Shift", true);
    }

    public void WallShiftSound()
    {
        AudioManager.instance.WallShiftSound();
    }

    public void DisableChangPos()
    {
        Destroy(this);
    }
}

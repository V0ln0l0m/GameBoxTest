using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    bool used = false;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Activation()
    {
        anim.SetBool("Press",true);
        if (!used)
        {
            used = true;
            ChangingPosition changPosScript = FindObjectOfType<ChangingPosition>();
            changPosScript.ChangPosition();
        }
    }

    public void Deactivation()
    {
        anim.SetBool("Press", false);
    }

    public void LeverSound()
    {
        AudioManager.instance.LeverSound();
    }
}

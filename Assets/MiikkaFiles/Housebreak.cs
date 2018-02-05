using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Housebreak : MonoBehaviour {

    public void Update()
    {
        Animator anim = GetComponent<Animator>();

        if (anim != null && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("break");
        }
        if (anim != null && Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("reverse");
        }
    }
}

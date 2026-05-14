using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShapeDefenseSpace.PublicData;

public class FrontAreaController : SceneSingleton<FrontAreaController>
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PannelActivate() {
        animator.SetBool(ANI_ACTIVATE, true);
    }

    public void PannelDown() {
        animator.SetBool(ANI_ACTIVATE, false);
    }
}

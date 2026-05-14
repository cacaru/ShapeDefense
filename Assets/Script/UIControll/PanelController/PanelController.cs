using UnityEngine;
using static ShapeDefenseSpace.PublicData;

public class PanelController : MonoBehaviour {
    
    private Animator animator;

    protected void SetAnimator(Animator ani) {
        animator = ani;
    }

    public virtual void PanelActivate() {
        animator.SetBool(ANI_ACTIVATE, true);
    }

    public virtual void PanelDown() {
        animator.SetBool(ANI_ACTIVATE, false);
    }
}
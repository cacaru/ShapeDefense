using UnityEngine;
using static ShapeDefenseSpace.PublicData;
using static ShapeDefenseSpace.GameData;

public class LibraryControll : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = Panel.GetComponent<Animator>();
    }

    // panel 보이기
    public void PanelActivate() {
        if(datahub.Gaming) {
            UnitClickObserver.Instance.Click_Off();
            //unitClickObserver.Click_Off();
            animator.SetBool(ANI_ACTIVATE, true);
        }
        else {
            animator.SetBool("StartPageActivate", true);
        }
    }
    // pannel 가리기
    public void PanelDown() {
        if (datahub.Gaming) {
            UnitClickObserver.Instance.Click_On();
            //unitClickObserver.Click_On();
            animator.SetBool(ANI_ACTIVATE, false);
        }
        else {
            animator.SetBool("StartPageActivate", false);
        }
    }

}

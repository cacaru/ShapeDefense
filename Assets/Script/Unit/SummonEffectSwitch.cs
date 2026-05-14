using System.Collections;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

public class SummonEffectSwitch : MonoBehaviour
{

    private GameObject EffectObj;
    private Field this_field;
    
    // Start is called before the first frame update
    void Start()
    {
        EffectObj = transform.Find("SummonEffect").gameObject;
        this_field = gameObject.GetComponent<Field>();
    }

    public void EffectOn() {
        EffectObj.SetActive(true);
        StartCoroutine(EffectOff());
    }

    IEnumerator EffectOff() {
        yield return wfs_0_5;
        EffectObj.SetActive(false);
        this_field.SettingField();

    }

}

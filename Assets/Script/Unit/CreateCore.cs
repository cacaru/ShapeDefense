using ShapeDefenseSpace;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

public class CreateCore : MonoBehaviour {
    
    public void CreateCoreClick() {
        if (datahub.LeftStageField <= 0 || datahub.CoreCount <= 0) {
            return;
        }
        UtilityHub.UnitCreateRandomField(301, 302, 303, 304, 305, 306);
        datahub.CoreCount--;
        CoreObserver.Instance.GetCoreOn();
    }

    public void CreateUniCoreClick() {
        if (datahub.LeftStageField <= 0 || datahub.UnicoreCount <= 0) {
            return;
        }
        UtilityHub.UnitCreateRandomField(401, 402, 403, 404, 405, 406);
        datahub.UnicoreCount--;
        CoreObserver.Instance.GetCoreOn();
    }
}
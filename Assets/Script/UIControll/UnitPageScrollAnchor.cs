
using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class UnitPageScrollAnchor : MonoBehaviour
{
    [SerializeField] private RectTransform ContentAnchoredRect;
    // Start is called before the first frame update
    void Start()
    {
        ContentAnchoredRect.anchoredPosition = datahub.Anchor;
    }

    public void RecodeAnchor() {
        datahub.Anchor = ContentAnchoredRect.anchoredPosition;
    }
}

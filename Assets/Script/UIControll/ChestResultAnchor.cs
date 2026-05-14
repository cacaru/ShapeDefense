using UnityEngine;


public class ChestResultAnchor : MonoBehaviour
{
    [SerializeField] private RectTransform ContentAnchoredRect;
    // Start is called before the first frame update

    private Vector2 anchor;

    public void AnchorReset() {
        ContentAnchoredRect.anchoredPosition = anchor;
    }
}
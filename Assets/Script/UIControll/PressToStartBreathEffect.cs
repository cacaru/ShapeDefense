using TMPro;
using UnityEngine;

public class PressToStartBreathEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text StartText;

    private readonly int MAXSIZE = 50;
    private readonly int MINSIZE = 40;
    private bool WAY = true;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        speed = 0.02f;
#elif UNITY_ANDROID
        speed = 0.2f;
#else
        speed = 0.02f;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (WAY) {
            StartText.fontSize += speed;
            if(StartText.fontSize >= MAXSIZE) { WAY = false; }
        }
        else {
            StartText.fontSize -= speed;
            if(StartText.fontSize <= MINSIZE) { WAY = true; }
        }
    }
}

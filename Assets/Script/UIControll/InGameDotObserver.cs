using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameDotObserver : MonoBehaviour
{

    [SerializeField]
    private TMP_Text dot;

    private DataHub datahub;
    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    // Update is called once per frame
    void Update()
    {
        dot.text = datahub.Dot.ToString();
    }
}

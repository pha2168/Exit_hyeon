using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUseTest : MonoBehaviour
{

    [SerializeField]
    private DayData dayData;
    public DayData DayData { set { dayData = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            UnityEngine.Debug.Log("Day : " + dayData.DayNum);
        }
    }
}

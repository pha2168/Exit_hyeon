using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuodioTset : MonoBehaviour
{
    private void OnEnable()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();

        foreach (AudioListener listener in listeners)
        {
            if (listener.gameObject != gameObject)
            {
                listener.enabled = false;
            }
        }

        GetComponent<AudioListener>().enabled = true;
    }
}

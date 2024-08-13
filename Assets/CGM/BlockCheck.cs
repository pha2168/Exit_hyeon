using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    public bool LeftCheck { get; private set; }
    public bool RightCheck { get; private set; }
    public bool ForwardCheck { get; private set; }
    public bool BackCheck { get; private set; }
    public bool DownCheck { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        LeftCheck = Physics.Raycast(transform.position, Vector3.left, out hit, 1.0f) && (hit.collider.CompareTag("LEFTSITE") || (hit.collider.CompareTag("BLOCK")));
        RightCheck = Physics.Raycast(transform.position, Vector3.right, out hit, 1.0f) && (hit.collider.CompareTag("RIGHTSITE") || (hit.collider.CompareTag("BLOCK")));
        ForwardCheck = Physics.Raycast(transform.position, Vector3.forward, out hit, 1.0f) && (hit.collider.CompareTag("FORWARDSITE") || (hit.collider.CompareTag("BLOCK")));
        BackCheck = Physics.Raycast(transform.position, Vector3.back, out hit, 1.0f) && (hit.collider.CompareTag("BACKSITE") || (hit.collider.CompareTag("BLOCK")));
        DownCheck = Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f) && (hit.collider.CompareTag("DOWNSITE")|| (hit.collider.CompareTag("BLOCK")));
    }
}

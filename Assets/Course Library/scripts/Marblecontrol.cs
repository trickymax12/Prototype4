using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marblecontrol : MonoBehaviour
{
    private Rigidbody PlayerRb;
    public float Speed = 5.0f;
    private GameObject FocalPoint;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        FocalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        PlayerRb.AddForce(FocalPoint.transform.forward * Speed * forwardInput);
    }
}

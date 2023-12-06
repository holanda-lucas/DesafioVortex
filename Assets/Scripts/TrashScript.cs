using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public int points;

    private Rigidbody rb;
    private Transform destination;
    private float suckerAttractionForce;

    private Transform antennaPoint;

    private GameObject sucker;

    public bool noGravity = false;
    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        sucker = GameObject.Find("Sucker");
        destination = sucker.transform.GetChild(1);

        antennaPoint = GameObject.Find("Antenna Point").transform;

        player = GameObject.Find("XR Origin (XR Rig)").transform;
    }

    private void Update()
    {
        if (GameController.haveAntenna && Vector3.Distance(transform.position, antennaPoint.position) > GameController.attractionRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, antennaPoint.position, 0.5f * Time.deltaTime);
        }

        if (noGravity)
        {
            rb.useGravity = false;
        }
    }

    void BeSucked()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.position, suckerAttractionForce * Time.deltaTime);
        rb.angularDrag = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "suck")
        {
            BeSucked();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        rb.angularDrag = 0.05f;
    }

    private void OnTriggerEnter(Collider other)
    {
        suckerAttractionForce = sucker.GetComponent<SuckerScript>().attractionForce;
    }
}
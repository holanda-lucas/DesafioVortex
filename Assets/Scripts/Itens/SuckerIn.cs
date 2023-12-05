using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckerIn : MonoBehaviour
{
    private SuckerScript suckerScript;

    private void Start()
    {
        suckerScript = transform.parent.GetComponent<SuckerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        suckerScript.EnterTrash(other);
    }
}

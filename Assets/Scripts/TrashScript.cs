using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BagIn":
                Destroy(gameObject);
                break;
        }
    }
}

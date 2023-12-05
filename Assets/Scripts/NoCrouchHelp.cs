using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NoCrouchHelp : MonoBehaviour
{
    public float radius;
    private Collider[] trashesFound;

    [SerializeField] private Transform rightHand;
 
    void Start()
    {
        
    }

    void Update()
    {
        trashesFound = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("trash"));

        foreach (Collider trash in trashesFound)
        {
            Transform tr = trash.transform;
            Rigidbody rb = tr.GetComponent<Rigidbody>();
            tr.GetComponent<TrashScript>().noGravity = true;

            float distance = rightHand.position.y - tr.position.y;

            /*
            if (distance > 0)
            {
                rb.AddForce(Vector3.up * distance * 10, ForceMode.Acceleration);
                Debug.Log(distance);
            }
            */


            if (rightHand.position.y > tr.position.y)
            {
                tr.position += new Vector3(0, Time.deltaTime * 5, 0);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

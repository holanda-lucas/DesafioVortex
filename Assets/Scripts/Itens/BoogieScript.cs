using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BoogieScript : MonoBehaviour
{
    private Collider[] peopleFound;
    private bool canExplode = false;
    private float timeFree = 0;

    private void Update()
    {
        // Desarmar a bomba 5 segundos depois dela ser largada
        if (canExplode)
        {
            timeFree += Time.deltaTime;

            if (timeFree >= 5)
            {
                timeFree = 0;
                canExplode = false;
            }
        }
    }
    void Explode()
    {
        // Iniciar dança em cada person
        foreach (Collider person in peopleFound)
        {
            person.gameObject.GetComponent<PersonScript>().Dance();
            person.gameObject.GetComponent<AudioSource>().Play();
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        peopleFound = Physics.OverlapSphere(transform.position, 3, LayerMask.GetMask("person"));

        if (peopleFound.Length > 0 && canExplode)
        {
            Explode();
        }
    }

    public void PermissionToExplode(SelectEnterEventArgs ev)
    {
        canExplode = true;
    }
}

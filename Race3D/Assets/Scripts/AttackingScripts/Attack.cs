using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float radius = 1f;
    public LayerMask layerMask;
    BoarController boarController;

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

        if (hits.Length > 0)
        {
            gameObject.SetActive(false);            

            if (hits[0].gameObject.tag == "Enemy")
            {
                hits[0].gameObject.GetComponent<BoarController>().SetHealth(damage);
                Debug.Log(hits[0].gameObject);
                hits[0].gameObject.GetComponent<BoarController>().Die();
            }
            else if(hits[0].gameObject.tag == "Player")
            {
                hits[0].gameObject.GetComponent<PlayerController>().SetHealth(damage);
                hits[0].gameObject.GetComponent<PlayerController>().Die();
            }
        }
    }
}

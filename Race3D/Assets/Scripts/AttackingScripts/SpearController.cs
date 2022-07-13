using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    public Rigidbody spearBody;
    public Transform shootTransform;

    private void Awake()
    {
        shootTransform = GetComponentInParent<Transform>();
    }
    void Start()
    {
        spearBody.velocity = 20f * shootTransform.forward;
    }

    void Update()
    {
        transform.up = Vector3.Slerp(transform.up, spearBody.velocity.normalized, Time.deltaTime * 15);
    }
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Enemy")
        {
            target.gameObject.GetComponent<BoarController>().SetHealth(100f);
            Debug.Log(target.gameObject);
            target.gameObject.GetComponent<BoarController>().Die();
        }
        else if (target.gameObject.tag == "Player")
        {
            target.gameObject.GetComponent<PlayerController>().SetHealth(50f);
            target.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}

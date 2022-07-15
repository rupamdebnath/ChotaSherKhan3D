using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    public Rigidbody spearBody;
    public Transform shootTransform;
    public GameObject tigerObject;
    private void Awake()
    {
        tigerObject = GameObject.FindGameObjectWithTag("Player");
        shootTransform = GetComponentInParent<Transform>();
    }
    void Start()
    {
        spearBody.velocity = 20f * shootTransform.forward;
        gameObject.GetComponent<AudioSource>().Play();
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
            target.gameObject.GetComponent<BoarController>().Die();
        }
        else if (target.gameObject.tag == "Player")
        {
            target.gameObject.GetComponent<PlayerController>().ReduceHealth(50f);
            target.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}

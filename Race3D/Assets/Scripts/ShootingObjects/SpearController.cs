using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    public Rigidbody spearBody;
    public Transform shootTransform;
    private void Awake()
    {
  
    }
    void Start()
    {
        spearBody.velocity = 15f * shootTransform.forward;
    }

    void Update()
    {
        transform.up = Vector3.Slerp(transform.up, spearBody.velocity.normalized, Time.deltaTime * 15);
    }

    private void FixedUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 50.0f;
    public Animator tigerAnimator;
    void Update()
    {
        float translation = Input.GetAxisRaw("Vertical") * speed;
        float rotation = Input.GetAxisRaw("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);

        transform.Rotate(0, rotation, 0);
        if (translation > 0f && Input.GetKey(KeyCode.LeftShift))
        {
            tigerAnimator.SetBool("Run", true);
            tigerAnimator.SetBool("Walk", false);
            speed = 5f;
        }
        else if (translation > 0f && Input.GetKeyUp(KeyCode.LeftShift))
        {
            tigerAnimator.SetBool("Walk", true);
            tigerAnimator.SetBool("Run", false);
            speed = 2f;
        }
        else if (translation > 0f)
        {
            tigerAnimator.SetBool("Walk", true);
            tigerAnimator.SetBool("Run", false);
            speed = 2f;
        }
        else
        {
            tigerAnimator.SetBool("Walk", false);
            tigerAnimator.SetBool("Run", false);
        }

    }
}

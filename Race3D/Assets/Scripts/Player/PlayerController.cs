using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Animator tigerAnimator;

    bool actionRunning = false;
    float translation, rotation;
    void Update()
    {
        if (actionRunning == false)
        {
            translation = Input.GetAxisRaw("Vertical") * speed;
            rotation = Input.GetAxisRaw("Horizontal") * rotationSpeed;

        }

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);

        transform.Rotate(0, rotation, 0);
        AnimationInputControls(); 
    }

    void AnimationInputControls()
    {
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
        else if (translation > 0f || translation < 0f)
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            tigerAnimator.SetTrigger("Strike");
            actionRunning = true;
            StartCoroutine(WaitForAction(1.5f));
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            tigerAnimator.ResetTrigger("Strike");

        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            tigerAnimator.SetTrigger("Roar");
            actionRunning = true;
            StartCoroutine(WaitForAction(3f));
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            tigerAnimator.ResetTrigger("Roar");
        }
    }
    IEnumerator WaitForAction(float seconds)
    {        
        yield return new WaitForSeconds(seconds);
        actionRunning = false;
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.CompareTag("Home"))
        {
            Debug.Log("Reached");
        }
    }
}

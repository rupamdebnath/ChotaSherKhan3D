using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DisableCam());
    }

    IEnumerator DisableCam()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
}

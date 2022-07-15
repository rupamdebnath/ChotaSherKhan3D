using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        StartCoroutine(StartLobbyScene());
    }

    IEnumerator StartLobbyScene()
    {
        yield return new WaitForSeconds(1);
        SceneLoader.Instance.LoadScene(1);
    }
}

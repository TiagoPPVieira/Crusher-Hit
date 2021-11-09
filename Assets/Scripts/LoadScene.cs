using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;

    public GameObject LoadingObj;

    public bool isBlocked;

    private void Awake()
    {
        instance = this;
    }

    public void ChooseScene(int scene)
    {
        if (!isBlocked)
        {
            StartCoroutine(LoadNewScene(scene));
        }
    }

    IEnumerator LoadNewScene(int scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene + 1);

        while (!async.isDone)
        {
            LoadingObj.SetActive(true);
            yield return null;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class UIManager : MonoBehaviour
{
    public CanvasGroup[] screens;
    public float FadeScreenTime = 0.2f;

    private int activeScreenInt = 0;
    private CanvasGroup ActiveScreen => screens[activeScreenInt];
    private CanvasGroup NextScreen => screens[activeScreenInt + 1];

    public List<string> serverData;

    void Start()
    {
        //beispiel
        //lol kommentar auf deutsch
        DataLoadingAndSaving.RequestDatafromServer();
        DataLoadingAndSaving.OnDataRequestComplete += () =>
        {
            serverData = DataLoadingAndSaving.GetAllEntriesfromKey("1");
        };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToNextScreen()
    {
        NextScreen.gameObject.SetActive(true);
        NextScreen.alpha = 0;
        FadeToNextCanvasGroup();

    }

    // async Task Test()
    // {
    //     await Task.Yield();
    // }

    private void FadeToNextCanvasGroup()
    {
        StartCoroutine(FadeCanvasGroup(screens[activeScreenInt], 1, 0, FadeScreenTime, true, () =>
            StartCoroutine(FadeCanvasGroup(screens[activeScreenInt], 0, 1, FadeScreenTime))));
    }


    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startValue, float targetValue, float time, bool disableWhenFinished = false, Action callback = null)
    {
        float timer = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            float value = Mathf.Lerp(startValue, targetValue, timer / time);
            canvasGroup.alpha = value;
            yield return null;
        }
        canvasGroup.alpha = targetValue;

        if (disableWhenFinished)
            canvasGroup.gameObject.SetActive(false);

        if (callback != null)
        {
            activeScreenInt++;
            callback.Invoke();
        }

    }

    // private IEnumerator FadeScreens(CanvasGroup canvasGroup1, CanvasGroup canvasGroup2, float time)
    // {
    //     float timer = 0;
    //     //float startValue = canvasGroup1
    //     while (timer < time)
    //     {
    //         timer += Time.deltaTime;
    //         float value = Mathf.Lerp(startValue, targetValue, timer / time);
    //         canvasGroup1.alpha = value;
    //         yield return null;
    //     }
    //     canvasGroup.alpha = targetValue;
    //
    //
    // }

}
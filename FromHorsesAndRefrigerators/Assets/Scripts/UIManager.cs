using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public CanvasGroup[] screens;
    public float FadeScreenTime = 0.2f;
    public float WaitBeforeVoiceOver = 0.3f;
    public float WaitAfterVoiceOver = 0.7f;

    [HideInInspector] public int ActiveScreenInt = 0;

    private int _activeChapter = 0;
    public int ActiveChapter
    {
        get
        {
            var parentName = transform.parent.name;
            if (parentName.Contains("Chapter"))
            {
                var number = parentName[^2].ToString();
                _activeChapter = int.Parse(parentName);
                return _activeChapter;
            }
            _activeChapter = 0;
            return 0;
        }
        set => _activeChapter = value;
    }

    private CanvasGroup ActiveScreen => screens[ActiveScreenInt];
    private CanvasGroup NextScreen => screens[ActiveScreenInt + 1];

    public List<string> serverData;

    public List<Chapter> chapters;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ActiveScreenInt = Array.FindIndex(screens, x => x.gameObject.activeInHierarchy);

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
        // NextScreen.gameObject.SetActive(true);
        // NextScreen.alpha = 0;
        FadeToNextCanvasGroup();

    }

    private void FadeToNextCanvasGroup()
    {
        StartCoroutine(FadeCanvasGroup(screens[ActiveScreenInt], 1, 0, FadeScreenTime, true, () =>
            StartCoroutine(FadeCanvasGroup(screens[ActiveScreenInt], 0, 1, FadeScreenTime))));
    }


    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startValue, float targetValue, float time, bool disableWhenFinished = false, Action callback = null)
    {
        canvasGroup.gameObject.SetActive(true);

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
            ActiveScreenInt++;
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
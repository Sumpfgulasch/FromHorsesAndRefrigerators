using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScreenAfterTime : MonoBehaviour
{
    public float maxTime = 5f;

    void Start()
    {
        StartCoroutine(QuitAfterTime(maxTime));
    }


    private IEnumerator QuitAfterTime(float maxTime)
    {
        yield return new WaitForSeconds(maxTime);
        UIManager.Instance.FadeToNextScreen();
    }
}
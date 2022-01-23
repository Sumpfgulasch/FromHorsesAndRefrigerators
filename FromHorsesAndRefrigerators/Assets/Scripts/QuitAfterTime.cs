using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAfterTime : MonoBehaviour
{
    public float maxTime = 2f;

    void Start()
    {
        StartCoroutine(Quit(maxTime));
    }



    private IEnumerator Quit(float time)
    {
        yield return new WaitForSeconds(time);

        Application.Quit();
    }
}
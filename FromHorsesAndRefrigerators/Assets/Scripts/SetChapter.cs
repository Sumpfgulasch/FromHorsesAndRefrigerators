using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChapter : MonoBehaviour
{
    public int chapter;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ActiveChapter = chapter;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
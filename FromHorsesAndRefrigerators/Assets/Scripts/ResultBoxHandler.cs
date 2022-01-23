using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultBoxHandler: MonoBehaviour
{
    public List<ResultBox> resultBoxes;

    private void OnEnable()
    {
	    PopulateresultBoxes();
    }

    public void PopulateresultBoxes()
    {
		for (int i = 0; i < UIManager.Instance.chapters.Count; i++)
		{
			resultBoxes[i]?.PopulateForChapter(UIManager.Instance.chapters[i]);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			PopulateresultBoxes();
		}
	}

}
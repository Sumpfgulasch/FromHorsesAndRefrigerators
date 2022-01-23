using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultBoxHandler: MonoBehaviour
{
    public List<ResultBox> resultBoxes;

    public void PopulateresultBoxes()
    {
		for (int i = 0; i < UIManager.Instance.chapters.Count - 1; i++)
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

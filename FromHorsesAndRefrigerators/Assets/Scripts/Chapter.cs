using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chapter
{
	public int ChapterID;
	public string StoryText;
	public string ShortStoryText;
	public string ServerShortAnswerKey { get { return "Short" + ChapterID; } }
	public string ServerLongAnswerKey { get { return "Long" + ChapterID; } }
	public string PlayerShortAnswer;
	public string PlayerLongAnswer;
	public AudioClip VoiceOver;

	public void SaveShortAnswer(string answer) 
	{
		PlayerShortAnswer = answer;
		DataLoadingAndSaving.AddEntryToKey(ServerShortAnswerKey, answer);
	}

	public void SaveLongAnswer(string answer) 
	{
		PlayerLongAnswer = answer;
		DataLoadingAndSaving.AddEntryToKey(ServerLongAnswerKey, answer);
	}
}

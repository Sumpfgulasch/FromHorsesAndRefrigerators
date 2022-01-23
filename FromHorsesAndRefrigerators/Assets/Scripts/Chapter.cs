using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chapter
{
	public int ChapterID;
	public string SotryText;
	private  int serverShortAnswerKey;
	private int serverLongAnswerKey;
	public string PlayerShortAnswer;
	public string PlayerLongAnswer;
	public AudioClip VoiceOver;

}

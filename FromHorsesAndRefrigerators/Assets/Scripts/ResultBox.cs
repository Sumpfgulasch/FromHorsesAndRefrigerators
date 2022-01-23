using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class ResultBox : MonoBehaviour
{
    public TextMeshProUGUI StoryText;
    public TextMeshProUGUI ShortAnswerInfoText;
    public TextMeshProUGUI YourLongAnswer;
    public TextMeshProUGUI OtherLongAnswers;

    private float percentOfPositiveAnswers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PopulateForChapter(Chapter chapter)
    {
        StoryText.text = chapter.ShortStoryText;

        // short answer info

        string shortAnswerMeaning = string.Empty;
        int percentToDisplay = 0;

        if (chapter.PlayerShortAnswer == "1") 
        {
            shortAnswerMeaning = "positive";
            percentToDisplay = (int) (100 * GetPercentOfPositiveAnswersforChapter(chapter));

        }
		else 
        {
            shortAnswerMeaning = "negative";
            percentToDisplay = 100 - (int)(100 * GetPercentOfPositiveAnswersforChapter(chapter));
        }


        ShortAnswerInfoText.text =
            "You have answered:" + shortAnswerMeaning + " togther with" + percentToDisplay + "% of players."; 

        // your long answer
        YourLongAnswer.text = chapter.PlayerLongAnswer;
        // other answers
        List<string> otherPeoplesRandomAnswers = DataLoadingAndSaving.GetRandomEntriesFromKey(chapter.ServerLongAnswerKey, 3);
        string OtherAnswers = string.Empty;

        for (int i = 0; i < otherPeoplesRandomAnswers.Count; i++)
        {
            OtherAnswers += (i + 1).ToString() + "." + otherPeoplesRandomAnswers[i] + "/n";
        }
    }

	public float GetPercentOfPositiveAnswersforChapter(Chapter chapter)
	{
        //get all answers
        List<string> allAnswers = DataLoadingAndSaving.GetAllEntriesfromKey(chapter.ServerShortAnswerKey);

        // convert to int list and add own answer
        List<int> answersAsInts = (List<int>)allAnswers.Select(str => int.Parse(str));
        answersAsInts.Add(int.Parse(chapter.PlayerShortAnswer));
        float sum = answersAsInts.Sum();

        return sum / (float)answersAsInts.Count;
       

	} 
}
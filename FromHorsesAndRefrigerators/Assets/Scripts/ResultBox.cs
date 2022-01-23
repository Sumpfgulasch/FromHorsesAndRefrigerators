using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class ResultBox : MonoBehaviour
{
    public TextMeshProUGUI StoryText;
    public TextMeshProUGUI ShortAnswerInfoText;
    //public TextMeshProUGUI YourLongAnswer;
    public TextMeshProUGUI OtherLongAnswers;
    public List<string> answerpreview;
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
            "You have answered: " + "<b><u>"+shortAnswerMeaning +"</b></u>" + " togther with " + percentToDisplay + "% of players, saying: \n" +
            $"<i>\"{chapter.PlayerLongAnswer}\"</i>"; 

        // your long answer
        //YourLongAnswer.text = $"<i>\"{chapter.PlayerLongAnswer}\"</i>";

        // other answers
        List<string> otherPeoplesRandomAnswers = new List<string>();
        otherPeoplesRandomAnswers = DataLoadingAndSaving.GetRandomEntriesFromKey(chapter.ServerLongAnswerKey, 3);
        string OtherAnswers = string.Empty;

        if (otherPeoplesRandomAnswers != null)
        {
            for (int i = 0; i < otherPeoplesRandomAnswers.Count; i++)
            {
                OtherAnswers += (i + 1).ToString() + "." + otherPeoplesRandomAnswers[i] + "\n";
            }
        }

        OtherLongAnswers.text = OtherAnswers;
        

    }

	public float GetPercentOfPositiveAnswersforChapter(Chapter chapter)
	{
        //get all answers
        List<string> allAnswers = DataLoadingAndSaving.GetAllEntriesfromKey(chapter.ServerShortAnswerKey);
        answerpreview = allAnswers;
        // convert to int list and add own answer
        List<int> answersAsInts = new List<int>();
        if (allAnswers != null)
        {
			foreach (var answer in allAnswers)
            {
                int result;
                if (int.TryParse(answer, out  result))
                {
                    answersAsInts.Add(result);
                }
			}
			
        }

        float sum = answersAsInts.Sum();

        return sum / (float)answersAsInts.Count;
       

	} 
}

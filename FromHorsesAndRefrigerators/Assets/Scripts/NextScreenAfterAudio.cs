using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScreenAfterAudio : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PlayAndNextScreen());
    }


    private IEnumerator PlayAndNextScreen()
    {
        Debug.Log(AudioManager.instance.voiceOver.isPlaying);
        if (AudioManager.instance.voiceOver.isPlaying)
            AudioManager.instance.voiceOver.Stop();

        yield return new WaitForSeconds(UIManager.Instance.WaitBeforeVoiceOver);

        var clip = UIManager.Instance.chapters[UIManager.Instance.ActiveScreenInt].VoiceOver;
        if (clip != null)
        {
            AudioManager.instance.voiceOver.clip = clip;
            AudioManager.instance.voiceOver.Play();

            yield return new WaitForSeconds(clip.length + UIManager.Instance.WaitAfterVoiceOver);
        }
        UIManager.Instance.FadeToNextScreen();
    }
}
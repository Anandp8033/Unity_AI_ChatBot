using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class TextToSpeech : MonoBehaviour
{
    private string azureKey = "413cac251d3a4e5b90c78d916194fb6a";
    private string azureRegion = "eastus";
    public Animator avatarAnimator;


    public async void Speak(string text , AudioSource audioSource)
    {
        var config = SpeechConfig.FromSubscription(azureKey, azureRegion);
        config.SpeechSynthesisVoiceName = "en-AU-WilliamNeural";
        config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Riff16Khz16BitMonoPcm);

        using var audioStream = AudioOutputStream.CreatePullStream();
        using var audioConfig = AudioConfig.FromStreamOutput(audioStream);
        using var synthesizer = new SpeechSynthesizer(config, audioConfig);

        var result = await synthesizer.SpeakTextAsync(text);

        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            Debug.Log("Speech Synthesized");

            byte[] audioData = result.AudioData;
            AudioClip clip = WavUtility.ToAudioClip(audioData);
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(AnimateWhileSpeaking(audioSource));

        }
        else
        {
            Debug.LogError($"Speech synthesis failed: {result.Reason}");
        }
    }

    IEnumerator AnimateWhileSpeaking(AudioSource audioSource)
    {
        avatarAnimator.SetBool("Talk", true);

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        avatarAnimator.SetBool("Talk", false);
    }


}

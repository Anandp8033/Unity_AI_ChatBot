using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using System.Threading.Tasks;

public class SpeechToText : MonoBehaviour
{
    private string azureKey = "413cac251d3a4e5b90c78d916194fb6a";
    private string azureRegion = "eastus";

     private LLMChat LLMChat;

    private void Start()
    {
        LLMChat = GetComponent<LLMChat>();
    }

    public async void StartRecognition()
    {
        var config = SpeechConfig.FromSubscription(azureKey, azureRegion);
        using var recognizer = new SpeechRecognizer(config);

        var result = await recognizer.RecognizeOnceAsync();
        if (result.Reason == ResultReason.RecognizedSpeech)
        {
            Debug.Log("Recognized Text: " + result.Text);
            LLMChat.SendToLLM(result.Text);
        }
        else
        {
            Debug.Log("Speech recognition failed.");
        }
    }
}

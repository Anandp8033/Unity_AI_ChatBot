using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;

public class LLMChat : MonoBehaviour
{
    private string openrouterKey = "Bearer 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";
    [SerializeField]
    private TextToSpeech TextToSpeech;
    [SerializeField]
    private AudioSource AudioSource;

    private void Start()
    {
        TextToSpeech = GetComponent<TextToSpeech>();
        AudioSource = FindAnyObjectByType<AudioSource>();
    }

    public void SendToLLM(string userInput)
    {
        StartCoroutine(SendPrompt(userInput));
    }

    IEnumerator SendPrompt(string prompt)
    {
        string url = "https://openrouter.ai/api/v1/chat/completions";
        string json = "{\"model\": \"moonshotai/kimi-k2:free\", \"messages\": [{\"role\": \"user\", \"content\": \"" + EscapeJson(prompt) + "\"}]}";

        Debug.Log(json);

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Authorization", openrouterKey);
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(req.downloadHandler.text);
            string responseText = ExtractContent(req.downloadHandler.text);
            Debug.Log("LLM Response: " + responseText);
            TextToSpeech.Speak(responseText, AudioSource);
        }
        else
        {
            Debug.Log("LLM Request Failed: " + req.error);
        }
    }

    string ExtractContent(string json)
    {
        // Basic extraction — consider using a JSON parser like Newtonsoft.Json for robustness
        var parsed = JSON.Parse(json);
        var content = parsed["choices"][0]["message"]["content"];
        return content != null ? content : "No response received.";

    }

    string EscapeJson(string input)
    {
        return input.Replace("\"", "\\\"").Replace("\n", "\\n");
    }

}

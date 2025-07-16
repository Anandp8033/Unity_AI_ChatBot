using System.Drawing.Printing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private SpeechToText speechToText;
    //[SerializeField]
    //private TextToSpeech TextToSpeech;
    //[SerializeField]    
    //private AudioSource audioSource;
    [SerializeField]
    private LLMChat LLMChat;
    [SerializeField]
    private TMP_InputField InputField;


    private void Awake()
    {
       // audioSource = FindAnyObjectByType<AudioSource>();
        speechToText = GetComponent<SpeechToText>();
       // TextToSpeech = GetComponent<TextToSpeech>();
        LLMChat = GetComponent<LLMChat>();
    }

    public void sendTextToLLM()
    {
        Debug.Log("InputField.text " + InputField.text);
        LLMChat.SendToLLM(InputField.text);
    }

    public void startRecogniseVoice()
    {
        speechToText.StartRecognition();
    } 
}

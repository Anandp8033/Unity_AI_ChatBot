using UnityEngine;

public class AvatarSpeakTest : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator=transform.GetChild(0).GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAudio()
    {
        audioSource.Play();
        animator.SetBool("Talk", true);
    }
    public void stopPlay()
    {
        audioSource.Stop();
        animator.SetBool("Talk", false);
    }
}

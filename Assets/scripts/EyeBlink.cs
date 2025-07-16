using System.Collections;
using UnityEngine;

public class EyeBlink : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer eyelid;
    [SerializeField]
    private SkinnedMeshRenderer head;

    [SerializeField]
    private int eyeLidIndexLeft;
    [SerializeField]
    private int eyeLidIndexRight;
    [SerializeField]
    private int headeEyeIndexLeft;
    [SerializeField]
    private int headeEyeIndexRight;


    public float blinkSpeed = 0.1f; // Time to fully blink

    void Start()
    {
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            yield return Blink();
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Wait between blinks
        }
    }

    IEnumerator Blink()
    {
        // Closing eyelid (0 to 1)
        for (float t = 0; t <= 1f; t += Time.deltaTime / blinkSpeed)
        {
            float value = Mathf.Lerp(0f, 100f, t); // BlendShape weight goes from 0 to 100
            SetBlinkWeight(value);
            yield return null;
        }

        // Opening eyelid (1 to 0)
        for (float t = 0; t <= 1f; t += Time.deltaTime / blinkSpeed)
        {
            float value = Mathf.Lerp(100f, 0f, t);
            SetBlinkWeight(value);
            yield return null;
        }
    }

    void SetBlinkWeight(float weight)
    {
        eyelid.SetBlendShapeWeight(eyeLidIndexLeft, weight);
        eyelid.SetBlendShapeWeight(eyeLidIndexRight, weight);
        head.SetBlendShapeWeight(headeEyeIndexLeft, weight);
        head.SetBlendShapeWeight(headeEyeIndexRight, weight);
    }


}

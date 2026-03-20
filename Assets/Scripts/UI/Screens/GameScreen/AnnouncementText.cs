using System.Collections;
using TMPro;
using UnityEngine;

public class AnnouncementText : MonoBehaviour
{
    [SerializeField]
    private UIAnimationComponent fadeAnimation;
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private float letterDelay = 0.05f;

    private Coroutine currentCoroutine;

    public void ShowText(string message)
    {
        fadeAnimation.PlayInAnimations();
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(AnimateText(message));
    }

    private IEnumerator AnimateText(string message)
    {
        text.text = "";
        for (int i = 0; i < message.Length; i++)
        {
            text.text += message[i];
            yield return new WaitForSeconds(letterDelay);
        }
        HideText();
    }

    public void HideText()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        fadeAnimation.SetOutDelay(text.text.Length / 10);
        fadeAnimation.PlayOutAnimations();
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class UINotifications : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeTime = 0.4f;
    [SerializeField] float stayTime = 1.5f;

    Coroutine routine;

    public void Show(string message)
    {
        if (routine != null)
            StopCoroutine(routine);

        text.text = message;
        gameObject.SetActive(true);
        routine = StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        yield return Fade(0f, 1f);
        yield return new WaitForSeconds(stayTime);
        yield return Fade(1f, 0f);
        gameObject.SetActive(false);
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        canvasGroup.alpha = from;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}

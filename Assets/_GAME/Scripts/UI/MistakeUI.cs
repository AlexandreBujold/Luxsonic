using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MistakeUI : MonoBehaviour
{
    [SerializeField]
    private float timeDisplayed = 2f;
    [SerializeField]
    private float fadeTime = 1f;
    [SerializeField]
    private CanvasGroup canvasGroup = null;

    private Coroutine displayCoroutine = null;

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    public void DisplayMistake()
    {
        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);

        displayCoroutine = StartCoroutine(DisplayThenFade());
    }

    private IEnumerator DisplayThenFade()
    {
        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(timeDisplayed);

        for (float time = 0; time < fadeTime; time+= Time.deltaTime)
        {
            float t = time / fadeTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}

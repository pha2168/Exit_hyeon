using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using System;

public class QuestSuccessLogVisible : MonoBehaviour
{
    public GameObject NomalLog1;
    public GameObject NomalLog2;
    public GameObject NomalLog3;
    public GameObject PseudoLog;
    public GameObject CriminalLog;
    public GameObject RevolutionLog;
    public GameObject GovernmentLog;

    private GameObject[] logs;
    private GameObject activeLog;

    public float fadeDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        logs = new GameObject[] { NomalLog1, NomalLog2, NomalLog3, PseudoLog, CriminalLog, RevolutionLog, GovernmentLog };

        // 모든 로그를 시작 시 비활성화합니다.
        foreach (var log in logs)
        {
            if (log != null)
            {
                log.SetActive(false);
                CanvasGroup canvasGroup = log.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = 0f;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateLog(int LogIndex)
    {
        if (logs.Length > 0 && LogIndex >= 0 && LogIndex < logs.Length)
        {
            if (activeLog != null)
            {
                StartCoroutine(FadeOut(activeLog, fadeDuration));
            }

            activeLog = logs[LogIndex];
            activeLog.SetActive(true);
            StartCoroutine(FadeIn(activeLog, fadeDuration));

            StartCoroutine(DesLogAfterDelay(activeLog, 3f + fadeDuration));
        }
    }

    private IEnumerator DesLogAfterDelay(GameObject log, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (log != null)
        {
            StartCoroutine(FadeOut(log, fadeDuration));
        }
    }

    private IEnumerator FadeIn(GameObject log, float duration)
    {
        CanvasGroup canvasGroup = log.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            float startTime = Time.time;
            while (Time.time < startTime + duration)
            {
                float t = (Time.time - startTime) / duration;
                canvasGroup.alpha = Mathf.Lerp(0, 1, t);
                yield return null;
            }
            canvasGroup.alpha = 1;
        }
    }

    private IEnumerator FadeOut(GameObject log, float duration)
    {
        CanvasGroup canvasGroup = log.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            float startTime = Time.time;
            while (Time.time < startTime + duration)
            {
                float t = (Time.time - startTime) / duration;
                canvasGroup.alpha = Mathf.Lerp(1, 0, t);
                yield return null;
            }
            canvasGroup.alpha = 0;
            log.SetActive(false);
        }
    }
}

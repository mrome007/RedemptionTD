using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RedemptionTDTimer : MonoBehaviour 
{
    public float CurrentTime { get { return currentTime; } }
    private float currentTime;
    private bool timerStarted;
    private StringBuilder timerBuffer;
    private const string timerFormat = "{0}:{1}:{2}";
    [SerializeField]
    private Text timerText;

    private void Awake()
    {
        timerBuffer = new StringBuilder();
    }

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        timerStarted = true;
        currentTime = 0f;
    }

    public void StopTimer()
    {
        timerStarted = false;
    }

    private void Update()
    {
        if(!timerStarted)
        {
            return;
        }

        timerBuffer.Length = 0;
        currentTime += Time.deltaTime;
        timerBuffer.AppendFormat(timerFormat, Mathf.Floor(currentTime / 60f).ToString("00"), 
                                (Mathf.Floor(currentTime) % 60).ToString("00"), 
                                ((currentTime % 1f) * 100f).ToString("00"));
        timerText.text = timerBuffer.ToString();
    }
}

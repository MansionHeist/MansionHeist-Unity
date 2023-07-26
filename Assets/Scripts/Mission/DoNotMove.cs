using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoNotMove : MonoBehaviour
{
    public float timerDuration = 4f;
    public string location;
    public AlarmUI alarm;
    public TMP_Text timerText;
    public Button button;

    private bool isButtonPressed = false;
    private Coroutine timerCoroutine;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        StartTimer();
    }

    private void OnEnable()
    {
        // Reset everything when the UI is set to active
        isButtonPressed = false;
        timerText.text = Mathf.CeilToInt(timerDuration).ToString();
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
        StartTimer();
    }

    private void OnButtonClick()
    {
        if (!isButtonPressed)
        {
            isButtonPressed = true;
            // Stop the timer coroutine if it's running
            if (timerCoroutine != null)
                StopCoroutine(timerCoroutine);
          
            timerText.text = "";
            MissionUI missionUI = GetComponent<MissionUI>();
            missionUI.FailClose(location);
        }
    }

    public void StartTimer()
    {
        // Start the timer coroutine
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        float remainingTime = timerDuration;

        while (remainingTime > 0f)
        {
            // Update the timer text with the remaining time
            timerText.text = Mathf.CeilToInt(remainingTime).ToString();
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        // Check if the button was pressed during the timer
        if (!isButtonPressed)
        {
            // If the button was not pressed, close the UI
            CloseUI();
        }
    }

    private void CloseUI()
    {
       
        MissionUI missionUI = GetComponent<MissionUI>();
        missionUI.SuccessClose(10);
    }
}

using TMPro;
using UnityEngine;

public class Classic : GameEvent
{
    [SerializeField, Space]
    private float timeLimit = 182f;
    [SerializeField]
    private TextMeshProUGUI timerText;

    private float elapsedTime = 0f;

    public override void EndEvent(bool isWin)
    {
        EventManager.OnEventEnded.Invoke(isWin);
    }

    public override void StartEvent()
    {
        elapsedTime = 0;
    }

    public override void UpdateEvent()
    {
        elapsedTime += Time.deltaTime;
        float remainingTime = Mathf.Max(0, timeLimit - elapsedTime);

        if(remainingTime <= 0f)
        {
            EndEvent(false);
            return;
        }

        int minutes = (int)(remainingTime / 60f);
        int seconds = (int)(remainingTime % 60f);

        timerText.text = $"{minutes}:{seconds:00}";
    }
}

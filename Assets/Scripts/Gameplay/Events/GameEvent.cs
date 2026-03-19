using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    [SerializeField]
    protected string startMessage;
    [SerializeField]
    protected string victoryMessage;
    [SerializeField]
    protected string defeatMessage;
    [SerializeField]
    protected GameObject eventContainer;

    public bool IsEventActive { get; private set; } = false;

    public abstract void StartEvent();
    public abstract void UpdateEvent();
    public abstract void EndEvent(bool isWin);

    public void ToggleEvent(bool isOn)
    {
        IsEventActive = isOn;
    }

    private void Start()
    {
        EventManager.OnGameStarted.AddListener(OnGameStarted);
    }

    private void OnGameStarted()
    {
        eventContainer.SetActive(IsEventActive);
    }
}

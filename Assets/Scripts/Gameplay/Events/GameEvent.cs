using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    [SerializeField]
    protected string startMessage;
    [SerializeField]
    protected string victoryMessage;
    [SerializeField]
    protected string defeatMessage;

    public abstract void StartEvent();
    public abstract void UpdateEvent();
    public abstract void EndEvent(bool isWin);
}

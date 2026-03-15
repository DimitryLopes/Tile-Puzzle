using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    public abstract void StartEvent();
    public abstract void UpdateEvent();
    public abstract void EndEvent(bool isWin);
}

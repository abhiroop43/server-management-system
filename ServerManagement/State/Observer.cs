namespace ServerManagement.State;

public class Observer
{
    protected Action? _listeners;

    public void AddStateChangeListeners(Action? listener)
    {
        if (listener is not null)
        {
            _listeners += listener;
        }
    }

    public void RemoveStateChangeListener(Action? listener)
    {
        if (listener is not null)
        {
            _listeners -= listener;
        }
    }

    public void BroadcastChange()
    {
        _listeners?.Invoke();
    }
}

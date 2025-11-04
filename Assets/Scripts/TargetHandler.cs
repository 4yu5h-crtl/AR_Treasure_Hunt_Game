using UnityEngine;
using Vuforia;

[RequireComponent(typeof(ObserverBehaviour))]
public class TargetHandler : MonoBehaviour
{
    public string clueId;
    public ClueManager clueManager;

    private ObserverBehaviour observer;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();
        if (observer != null)
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            if (clueManager != null)
                clueManager.OnTargetFound(clueId, this.transform);
            else
                Debug.LogWarning($"TargetHandler: ClueManager not assigned for {clueId}");
        }
    }

    void OnDestroy()
    {
        if (observer != null)
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
    }
}

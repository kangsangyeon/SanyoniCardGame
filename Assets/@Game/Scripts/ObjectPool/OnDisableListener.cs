using UnityEngine;
using UnityEngine.Events;

public class OnDisableListener : MonoBehaviour
{
    public UnityEvent<GameObject> OnDisableEvent = new UnityEvent<GameObject>();

    private void OnDisable()
    {
        OnDisableEvent.Invoke(gameObject);
    }
}
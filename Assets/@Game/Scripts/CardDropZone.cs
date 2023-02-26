using System;
using UnityEngine;
using UnityEngine.Events;

public class CardDropZone : MonoBehaviour
{
    [SerializeField] private Transform m_DropPosition;
    [SerializeField] private UnityEvent<CardDrag> m_OnDropEvent;

    private void Awake()
    {
        if (m_DropPosition == null)
            m_DropPosition = transform;
    }

    public Transform GetDropPosition() => m_DropPosition;
    public UnityEvent<CardDrag> GetOnDropEvent() => m_OnDropEvent;
}
using System;
using UnityEngine;
using UnityEngine.Events;

public class CardDropZone : MonoBehaviour
{
    [SerializeField] private Transform m_DropPosition;
    [SerializeField] private UnityEvent<CardDrag> m_OnDropEvent;
    [SerializeField] private bool m_bCanDrop = true;

    public Transform GetDropPosition() => m_DropPosition;
    public UnityEvent<CardDrag> GetOnDropEvent() => m_OnDropEvent;
    public bool GetCanDrop() => m_bCanDrop;
    public void SetCanDrop(bool _value) => m_bCanDrop = _value;

    private void Awake()
    {
        if (m_DropPosition == null)
            m_DropPosition = transform;
    }
}
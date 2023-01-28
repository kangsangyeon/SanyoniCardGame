using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDropZone : MonoBehaviour
{
    [SerializeField] private Transform m_DropPosition;
    [SerializeField] private Action<Card> m_OnDropEvent;

    private void Awake()
    {
        if (m_DropPosition == null)
            m_DropPosition = transform;
    }

    public Transform GetDropPosition() => m_DropPosition;
    public Action<Card> GetOnDropEvent() => m_OnDropEvent;
}
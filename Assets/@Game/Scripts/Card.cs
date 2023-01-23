using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float m_DragLerpSpeed = 0.05f;
    private bool m_bDrag;
    private Vector2 m_DragOffset;
    private Vector2 m_DragTargetPosition;

    private void Start()
    {
        m_DragTargetPosition = transform.position;
    }

    private void Update()
    {
        float _abs = Mathf.Abs(Vector2.Distance(transform.position, m_DragTargetPosition));
        if (_abs > 0.0001f)
        {
            transform.position = Vector2.Lerp(transform.position, m_DragTargetPosition, m_DragLerpSpeed);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DragOffset = (Vector2)transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_DragTargetPosition = eventData.position + m_DragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
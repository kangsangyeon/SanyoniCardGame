using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float m_DragLerpSpeed = 0.05f;
    [SerializeField] private float m_RotateLerpSpeed = 0.1f;

    private bool m_bMouseOver;
    private bool m_bDrag;
    private Vector2 m_DragOffset;
    private Vector2 m_DesiredPosition;
    private Quaternion m_DesiredRotation;
    private CardDropZone m_DesiredDropZone;
    private List<Collider2D> m_OverlappedTriggerList = new List<Collider2D>();
    private UnityAction<CardDrag, CardDropZone> m_OnDropZoneEvent;

    public bool IsMouseOver() => m_bMouseOver;
    public bool IsDrag() => m_bDrag;
    public void SetDesiredPosition(Vector2 _position) => m_DesiredPosition = _position;
    public void SetDesiredRotation(Quaternion _rotation) => m_DesiredRotation = _rotation;

    public UnityAction<CardDrag, CardDropZone> OnDropZoneEvent
    {
        get => m_OnDropZoneEvent;
        set => m_OnDropZoneEvent = value;
    }

    private void Start()
    {
        m_DesiredPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, m_DesiredPosition, m_DragLerpSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_DesiredRotation, m_RotateLerpSpeed);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_bDrag = true;
        m_DragOffset = (Vector2)transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_DesiredPosition = eventData.position + m_DragOffset;

        if (m_OverlappedTriggerList.Count > 0)
        {
            var _trigger = m_OverlappedTriggerList[m_OverlappedTriggerList.Count - 1];
            var _cardZone = _trigger.GetComponent<CardDropZone>();
            if (_cardZone)
            {
                m_DesiredDropZone = _cardZone;
            }
        }
        else
        {
            m_DesiredDropZone = null;
        }

        // 겹친 오브젝트들에 대해, 얼만큼 겹쳐있는지에 대한 퍼센티지를 계산합니다.
        // 일정 영역 이하로 겹친 오브젝트들을 걸러냅니다.
        // 가장 많이 겹친 오브젝트에 대해 snap 하이라이트를 보여줍니다.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_bDrag = false;
        if (m_DesiredDropZone)
        {
            m_DesiredPosition = m_DesiredDropZone.GetDropPosition().position;

            if (m_OnDropZoneEvent != null)
                m_OnDropZoneEvent.Invoke(this, m_DesiredDropZone);

            if (m_DesiredDropZone.GetOnDropEvent() != null)
                m_DesiredDropZone.GetOnDropEvent().Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) => m_OverlappedTriggerList.Add(col);
    private void OnTriggerExit2D(Collider2D other) => m_OverlappedTriggerList.Remove(other);

    private void OnMouseEnter() => m_bMouseOver = true;
    private void OnMouseExit() => m_bMouseOver = false;
}
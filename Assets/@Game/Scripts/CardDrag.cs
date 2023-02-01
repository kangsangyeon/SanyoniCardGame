using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float m_DragLerpSpeed = 0.05f;
    
    private bool m_bDrag;
    private Vector2 m_DragOffset;
    private Vector2 m_DesiredPosition;
    private Transform m_DesiredDropPosition;
    private List<Collider2D> m_OverlappedTriggerList = new List<Collider2D>();

    public Vector2 SetDesiredPosition(Vector2 _position) => m_DesiredPosition = _position;

    private void Start()
    {
        m_DesiredPosition = transform.position;
    }

    private void Update()
    {
        float _abs = Mathf.Abs(Vector2.Distance(transform.position, m_DesiredPosition));
        if (_abs > 0.0001f)
        {
            transform.position = Vector2.Lerp(transform.position, m_DesiredPosition, m_DragLerpSpeed);
        }
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
                m_DesiredDropPosition = _cardZone.GetDropPosition();
                if (_cardZone.GetOnDropEvent() != null)
                    _cardZone.GetOnDropEvent().Invoke(this);
            }
        }
        else
        {
            m_DesiredDropPosition = null;
        }

        // 겹친 오브젝트들에 대해, 얼만큼 겹쳐있는지에 대한 퍼센티지를 계산합니다.
        // 일정 영역 이하로 겹친 오브젝트들을 걸러냅니다.
        // 가장 많이 겹친 오브젝트에 대해 snap 하이라이트를 보여줍니다.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_bDrag = false;
        if (m_DesiredDropPosition)
        {
            m_DesiredPosition = m_DesiredDropPosition.position;
            m_DesiredDropPosition = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) => m_OverlappedTriggerList.Add(col);
    private void OnTriggerExit2D(Collider2D other) => m_OverlappedTriggerList.Remove(other);
}
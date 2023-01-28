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
    private Vector2 m_DragTargetPosition;
    private Transform m_DesiredDropTarget;
    private List<Collider2D> m_OverlappedTriggerList = new List<Collider2D>();

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
        Debug.Log(name);
        m_DragOffset = (Vector2)transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_DragTargetPosition = eventData.position + m_DragOffset;

        if (m_OverlappedTriggerList.Count > 0)
        {
            var _trigger = m_OverlappedTriggerList[m_OverlappedTriggerList.Count - 1];
            var _cardZone = _trigger.GetComponent<CardDropZone>();
            if (_cardZone)
            {
                m_DesiredDropTarget = _cardZone.GetDropPosition();
                _cardZone.GetOnDropEvent().Invoke(this);
            }
        }
        else
        {
            m_DesiredDropTarget = null;
        }

        // 겹친 오브젝트들에 대해, 얼만큼 겹쳐있는지에 대한 퍼센티지를 계산합니다.
        // 일정 영역 이하로 겹친 오브젝트들을 걸러냅니다.
        // 가장 많이 겹친 오브젝트에 대해 snap 하이라이트를 보여줍니다.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DesiredDropTarget)
        {
            m_DragTargetPosition = m_DesiredDropTarget.position;
            m_DesiredDropTarget = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) => m_OverlappedTriggerList.Add(col);
    private void OnTriggerExit2D(Collider2D other) => m_OverlappedTriggerList.Remove(other);
}
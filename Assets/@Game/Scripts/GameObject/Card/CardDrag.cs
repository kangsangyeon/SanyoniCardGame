using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private CardGameObject m_CardGO;
    [SerializeField] private Collider2D m_Collider;
    [SerializeField] private float m_DragLerpSpeed = 0.05f;
    [SerializeField] private float m_RotateLerpSpeed = 0.1f;
    [SerializeField] private float m_ScaleLerpSpeed = 0.1f;

    private bool m_bMouseOver;
    private bool m_bDrag;
    private bool m_bMovingPrevFrame;
    private Vector2 m_DragOffset;
    private Vector2 m_DesiredPosition;
    private Quaternion m_DesiredRotation;
    private Vector2 m_DesiredScale;
    private CardDropZone m_DesiredDropZone;
    private List<Collider2D> m_OverlappedTriggerList = new List<Collider2D>();
    private UnityEvent<CardDrag, CardDropZone> m_OnDropZoneEvent = new UnityEvent<CardDrag, CardDropZone>();
    private UnityEvent<CardDrag> m_OnEndMovementEvent = new UnityEvent<CardDrag>();

    public CardGameObject GetCardGO() => m_CardGO;
    public bool IsMouseOver() => m_bMouseOver;
    public bool IsDrag() => m_bDrag;
    public void SetDesiredPosition(Vector2 _position) => m_DesiredPosition = _position;
    public void SetDesiredRotation(Quaternion _rotation) => m_DesiredRotation = _rotation;
    public void SetDesiredScale(Vector2 _scale) => m_DesiredScale = _scale;

    public void SetDesiredTransform(Vector2 _position, Quaternion _rotation, Vector2 _scale)
    {
        m_DesiredPosition = _position;
        m_DesiredRotation = _rotation;
        m_DesiredScale = _scale;
    }

    public UnityEvent<CardDrag, CardDropZone> GetOnDropZoneEvent() => m_OnDropZoneEvent;
    public UnityEvent<CardDrag> GetOnEndMovementEvent() => m_OnEndMovementEvent;

    private void OnEnable()
    {
        m_DesiredPosition = transform.position;
        m_DesiredRotation = transform.rotation;
        m_DesiredScale = transform.localScale;

        m_CardGO.GetOnChangeInteractableEvent().AddListener(OnChangeInteractable);
    }

    private void OnDisable()
    {
        m_CardGO.GetOnChangeInteractableEvent().RemoveListener(OnChangeInteractable);
    }

    private void Update()
    {
        bool _bMoving = false;
        float _distance = Vector2.Distance(transform.position, m_DesiredPosition);
        if (_distance > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, m_DesiredPosition, m_DragLerpSpeed);
            _bMoving |= true;
        }
        else
        {
            transform.position = m_DesiredPosition;
        }

        float _angleDifference = Quaternion.Angle(transform.rotation, m_DesiredRotation);
        if (_angleDifference > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, m_DesiredRotation, m_RotateLerpSpeed);
            _bMoving |= true;
        }
        else
        {
            transform.rotation = m_DesiredRotation;
        }

        float _scaleDifference = Vector2.Distance(transform.localScale, m_DesiredScale);
        if (_scaleDifference > 0.01f)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, m_DesiredScale, m_ScaleLerpSpeed);
            _bMoving |= true;
        }
        else
        {
            transform.localScale = m_DesiredScale;
        }

        if (m_bMovingPrevFrame == true && _bMoving == false)
        {
            // 카드 움직임이 멈추었을 때 실행됩니다.
            m_OnEndMovementEvent.Invoke(this);
        }

        m_bMovingPrevFrame = _bMoving;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetCardGO().GetIsInteractable() == false)
            return;

        m_bDrag = true;
        m_DragOffset = (Vector2)transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GetCardGO().GetIsInteractable() == false)
            return;

        Vector2 _mousePosition = eventData.position;
        m_DesiredPosition = eventData.position + m_DragOffset;

        // 카드와 겹치는 drop zone 중,
        // 가장 가까운 거리에 있는 drop zone을 찾아
        // desired drop zone으로 설정합니다.

        float _nearestDistance = float.MaxValue;
        CardDropZone _desiredZone = null;

        for (int i = 0; i < m_OverlappedTriggerList.Count; ++i)
        {
            var _trigger = m_OverlappedTriggerList[i];
            var _cardZone = _trigger.GetComponent<CardDropZone>();
            if (_cardZone && _cardZone.GetCanDrop())
            {
                Vector2 _cardZonePosition = _cardZone.transform.position;
                float _distance = Vector2.Distance(_mousePosition, _cardZonePosition);
                if (_distance < _nearestDistance)
                {
                    _nearestDistance = _distance;
                    _desiredZone = _cardZone;
                }
            }
        }

        m_DesiredDropZone = _desiredZone;

        // 겹친 오브젝트들에 대해, 얼만큼 겹쳐있는지에 대한 퍼센티지를 계산합니다.
        // 일정 영역 이하로 겹친 오브젝트들을 걸러냅니다.
        // 가장 많이 겹친 오브젝트에 대해 snap 하이라이트를 보여줍니다.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GetCardGO().GetIsInteractable() == false)
            return;

        m_bDrag = false;
        if (m_DesiredDropZone)
        {
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

    private void OnChangeInteractable(bool _interactable)
    {
        m_Collider.enabled = _interactable;

        if (_interactable == false)
        {
            m_bDrag = false;
            m_bMouseOver = false;
            m_DesiredDropZone = null;
            m_OverlappedTriggerList.Clear();
        }
    }
}
using System.Collections.Generic;
using SanyoniLib.Bezier;
using UnityEngine;
using UnityEngine.Assertions;

public class CardLayout : MonoBehaviour
{
    [SerializeField] private float m_BezierCenterDown = 4000;
    [SerializeField] private float m_CardMouseOverUp = 200;
    [SerializeField] private float m_CardHideHeight = 120;
    [SerializeField] private Bezier m_Bezier;
    private List<CardGameObject> m_CardList = new List<CardGameObject>();
    private bool m_bVisible = false;

    public void SetVisibility(bool _visible)
    {
        m_bVisible = _visible;
    }

    public void AddCard(CardGameObject _cardGameObject)
    {
        var _notContains = m_CardList.Contains(_cardGameObject) == false;
        Assert.IsTrue(_notContains);
        m_CardList.Add(_cardGameObject);
        RefreshCardRenderOrder();
    }

    public void RemoveCard(CardGameObject _cardGameObject)
    {
        var _contains = m_CardList.Contains(_cardGameObject) == true;
        Assert.IsTrue(_contains);
        m_CardList.Remove(_cardGameObject);
        RefreshCardRenderOrder();
    }

    private void RefreshCardRenderOrder()
    {
        for (int i = 0; i < m_CardList.Count; ++i)
        {
            var _renderOrder = m_CardList[i].GetRenderOrder();
            _renderOrder.SetRenderOrder(m_CardList.Count - i);
            _renderOrder.SetLayerAsHand();
        }
    }

    private void Update()
    {
        Vector2 _bezierCenter = (m_Bezier.GetPoint(0).GetPosition() + m_Bezier.GetPoint(1).GetPosition()) / 2 + Vector3.down * m_BezierCenterDown;

        float _interval = 1.0f / (m_CardList.Count + 1);
        for (int i = 0; i < m_CardList.Count; ++i)
        {
            CardGameObject _cardGameObject = m_CardList[i];

            if (_cardGameObject.GetDrag().IsDrag())
                return;

            float _delta = _interval * (1 + i);
            Vector2 _position = m_Bezier.GetPoint(_delta).GetPosition();

            Vector2 _desiredPosition;
            Quaternion _desiredRotation;

            if (_cardGameObject.GetDrag().IsMouseOver())
            {
                _desiredPosition = _position + Vector2.up * m_CardMouseOverUp;
                _desiredRotation = Quaternion.identity;

                _cardGameObject.GetRenderOrder().SetRenderOrder(100);
            }
            else
            {
                Vector2 _direction = (_position - _bezierCenter);
                float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90;
                Quaternion _rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                _desiredPosition = _position;
                _desiredRotation = _rotation;
            }

            if (m_bVisible)
            {
                _cardGameObject.SetIsInteractable(true);
            }
            else
            {
                _cardGameObject.SetIsInteractable(false);
                _desiredPosition = _desiredPosition + Vector2.down * m_CardHideHeight;
            }

            m_CardList[i].GetDrag().SetDesiredPosition(_desiredPosition);
            m_CardList[i].GetDrag().SetDesiredRotation(_desiredRotation);

            Debug.DrawLine(_position, _desiredPosition, Color.blue);
            Debug.DrawLine(_bezierCenter, _position, Color.red);
        }
    }
}
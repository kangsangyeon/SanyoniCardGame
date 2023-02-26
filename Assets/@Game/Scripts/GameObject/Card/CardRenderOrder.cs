using System;
using UnityEngine;

public class CardRenderOrder : MonoBehaviour
{
    private int SortingLayerId_Hand;
    private int SortingLayerId_Field;

    [SerializeField] private Canvas m_Canvas;

    public void SetLayerAsHand() => m_Canvas.sortingLayerID = SortingLayerId_Hand;
    public void SetLayerAsField() => m_Canvas.sortingLayerID = SortingLayerId_Field;
    public void SetRenderOrder(int _order) => m_Canvas.sortingOrder = _order;

    private void Awake()
    {
        SortingLayerId_Hand = SortingLayer.NameToID("Hand");
        SortingLayerId_Field = SortingLayer.NameToID("Field");
    }
}
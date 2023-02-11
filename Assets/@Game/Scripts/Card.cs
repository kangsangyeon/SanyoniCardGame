using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardDrag m_Drag;
    [SerializeField] private CardRenderOrder m_RenderOrder;

    private List<CardOperationBase> m_EffectSequence;
    private int m_Cost;

    public CardDrag GetDrag() => m_Drag;
    public CardRenderOrder GetRenderOrder() => m_RenderOrder;
}
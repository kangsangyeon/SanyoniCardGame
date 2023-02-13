using System;
using System.Collections;
using System.Collections.Generic;

public class CardOperatino_SelectTarget_FromOrdered : CardOperationBase
{
    private EPlayerType m_TargetType;
    private EOrderedDummyType m_OrderedDummyType;
    private int m_Count;
    private bool m_FromUpside;

    public override IEnumerator Perform(Card _owner)
    {
        // m_TargetType = Enum.Parse<EPlayerType>(m_Arguments[0]);
        // m_OrderedDummyType = Enum.Parse<EOrderedDummyType>(m_Arguments[1]);
        // m_Count = int.Parse(m_Arguments[2]);
        //
        // CardDummy _dummy = GameManager.GetDummy(m_OrderedDummyType);
        //
        // List<Card> _cardList = new List<Card>();
        // for (int i = 0; i < m_Count && _dummy.IsEmpty() == false; ++i)
        // {
        //     _cardList.Add(_dummy.GetBackCard());
        // }

        yield break;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    struct MarketPair
    {
        public CardAttribute cardAttribute;
        public int count;
    }

    private Dictionary<int, CardDummy> m_PlayerDeckDict;
    private Dictionary<int, CardDummy> m_PlayerJunkDummyDict;
    private Dictionary<int, CardField> m_PlayerFieldDict;
    private CardDummy m_SupplyDummy;
    private MarketPair[] m_MarketList = new MarketPair[8];
}
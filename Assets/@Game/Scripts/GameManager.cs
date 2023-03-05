using System;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    struct MarketPair
    {
        public CardAttribute cardAttribute;
        public int count;
    }

    [SerializeField] private UI_CardDummy m_UI_CardDummy;

    [SerializeField] private CardDummy[] m_PlayerHandDummyList = new CardDummy[2];
    [SerializeField] private CardDummy[] m_PlayerDummyList = new CardDummy[2];
    [SerializeField] private CardDummy m_SupplyDummy;
    [SerializeField] private CardDummy m_OutlandDummy;
    private MarketPair[] m_MarketList = new MarketPair[8];

    public UI_CardDummy GetUICardDummy() => m_UI_CardDummy;
    public CardDummy GetPlayerHandDummy(int _index) => m_PlayerHandDummyList[_index];
    public CardDummy GetPlayerDummy(int _index) => m_PlayerDummyList[_index];
    public CardDummy GetSupplyDummy() => m_SupplyDummy;
    public CardDummy GetOutlandDummy() => m_OutlandDummy;

    private void Awake()
    {
        Assert.IsTrue(Instance == null);
        Instance = this;
    }

    private void OnDestroy()
    {
        Assert.IsTrue(Instance == this);
        Instance = null;
    }
}
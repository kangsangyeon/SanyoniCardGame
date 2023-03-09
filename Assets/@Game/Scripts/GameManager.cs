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

    [SerializeField] private UICanvas_CardDummy m_UI_CardDummy;
    [SerializeField] private UICanvas_MyStatus m_UI_MyStatus;

    [SerializeField] private PlayerContext[] m_PlayerContextList = new PlayerContext[2];
    [SerializeField] private CardDummy m_SupplyDummy;
    [SerializeField] private CardDummy m_OutlandDummy;
    private MarketPair[] m_MarketList = new MarketPair[8];

    public UICanvas_CardDummy GetUICardDummy() => m_UI_CardDummy;
    public PlayerContext GetPlayerContext(int _index) => m_PlayerContextList[_index];
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

    private void OnEnable()
    {
        // 0번째 플레이어는 자기 자신입니다.
        m_PlayerContextList[0].AddEventListeners();

        foreach (PlayerContext _context in m_PlayerContextList)
        {
            _context.GetOnChangeValueEvent().AddListener(OnChangePlayerContextValue);
        }
    }

    private void OnDisable()
    {
        // 0번째 플레이어는 자기 자신입니다.
        m_PlayerContextList[0].RemoveEventListeners();
        
        foreach (PlayerContext _context in m_PlayerContextList)
        {
            _context.GetOnChangeValueEvent().RemoveListener(OnChangePlayerContextValue);
        }
    }

    private void OnChangePlayerContextValue(PlayerContext _context)
    {
        m_UI_MyStatus.Refresh(_context);
    }
}
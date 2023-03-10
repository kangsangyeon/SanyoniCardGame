using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMarket : MonoBehaviour
{
    [SerializeField] private RandomMarketSupply m_RandomMarketSupply;
    [SerializeField] private CardLayout m_Layout;

    private IEnumerator Start()
    {
        // 공급 더미를 생성하고, 카드를 마켓 슬롯에 채워 넣습니다.
        m_RandomMarketSupply.CreateRandomMarketCardList();
        yield return StartCoroutine(m_RandomMarketSupply.FillRandomMarket());

        // 상품 구매 단계에서는 손에 든 카드가 상점을 가리지 않도록 아래로 내립니다.
        m_Layout.SetVisibility(false);

        // 상품 구매를 바로 테스트할 수 있도록 플레이어에게 100원을 쥐여줍니다.
        GameManager.Instance.GetPlayerContext(0).Gold += 100;
    }
}
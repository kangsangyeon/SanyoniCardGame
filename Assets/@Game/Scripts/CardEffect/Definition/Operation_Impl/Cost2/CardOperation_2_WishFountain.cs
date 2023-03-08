using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SanyoniCardGame/CardOperation/2_WishFountain(소원의 샘)")]
public class CardOperation_2_WishFountain : CardOperationBase
{
    public override IEnumerator Perform(CardGameObject _owner)
    {
        var _playerContext = GameManager.Instance.GetPlayerContext(0);
        CardDummy _drawPile = _playerContext.DrawPile;
        CardDummy _hand = _playerContext.Hand;
        CardDummy _discard = _playerContext.DiscardDummy;
        var _uiCardDummy = GameManager.Instance.GetUICardDummy();

        // 내 패에서 버릴 카드들을 지정합니다.
        _uiCardDummy.Show("추방시킬 카드 한 장을 선택하세요.", _hand, true, 0);

        // 카드들을 선택할 때까지 대기합니다.
        while (_uiCardDummy.GetIsSelectComplete() == false) yield return null;

        // 선택한 카드들을 버린 카드 더미로 이동합니다.
        List<Card> _selectedCards = _uiCardDummy.GetSelectedCards();
        _discard.AddCardList(_selectedCards);

        // 버린 카드들의 개수만큼 다시 드로우합니다.
        var _drawCards = _drawPile.Draw(_selectedCards.Count);
        _hand.AddCardList(_drawCards);

        yield return new WaitForSeconds(1);
    }
}
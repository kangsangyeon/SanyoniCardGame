using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SanyoniCardGame/CardOperation/2_Offering(기부)")]
public class CardOperation_2_Offering : CardOperationBase
{
    public override IEnumerator Perform(CardGameObject _owner)
    {
        // 손패의 카드 한 장을 선택해 국외로 이동시킵니다.

        CardDummy _hand = GameManager.Instance.GetPlayerContext(0).Hand;
        var _uiCardDummy = GameManager.Instance.GetUICardDummy();

        _uiCardDummy.Show("추방시킬 카드 한 장을 선택하세요.", _hand, true, 1);

        // 카드 한 장을 선택할 때까지 대기합니다.
        while (_uiCardDummy.GetIsSelectComplete() == false) yield return null;

        // 선택한 카드를 국외로 이동합니다.
        List<Card> _selectedCards = _uiCardDummy.GetSelectedCards();
        GameManager.Instance.GetOutlandDummy().AddCardList(_selectedCards);

        yield return new WaitForSeconds(1.0f);
    }
}
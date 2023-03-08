using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SanyoniCardGame/CardOperation/2_Horse(파발마)")]
public class CardOperation_2_Horse : CardOperationBase
{
    public override IEnumerator Perform(CardGameObject _owner)
    {
        var _playerContext = GameManager.Instance.GetPlayerContext(0);
        
        // 내 더미에서 한 장을 가져옵니다.
        var _card = _playerContext.DrawPile.Draw(1);
        
        // 내 손패에 넣습니다.
        _playerContext.Hand.AddCardList(_card);

        yield return null;
    }
}
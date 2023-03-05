using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SanyoniCardGame/CardOperation/2_Horse(파발마)")]
public class CardOperation_2_Horse : CardOperationBase
{
    public override IEnumerator Perform(CardGameObject _owner)
    {
        // 내 더미에서 한 장을 가져옵니다.
        var _card = GameManager.Instance.GetPlayerDummy(0).Draw(1);
        
        // 내 손패에 넣습니다.
        GameManager.Instance.GetPlayerHandDummy(0).AddCardList(_card);

        yield return null;
    }
}
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SanyoniCardGame/CardOperation/AcquireGold")]
public class CardOperation_AcquireGold : CardOperationBase
{
    public override IEnumerator Perform(CardGameObject _owner)
    {
        int _gold = int.Parse(m_Arguments[0]);

        var _playerContext = GameManager.Instance.GetPlayerContext(0);
        int _goldOrigin = _playerContext.Gold;
        int _newGold = _goldOrigin + _gold;
        _playerContext.Gold = _newGold;

        Debug.Log($"CardOperation_AcquireGold::Perform(): gold changed. {_goldOrigin} -> {_newGold}");

        yield return new WaitForSeconds(1);
    }
}
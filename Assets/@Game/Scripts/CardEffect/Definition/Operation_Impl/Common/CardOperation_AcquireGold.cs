using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SanyoniCardGame/CardOperation/AcquireGold")]
public class CardOperation_AcquireGold : CardOperationBase
{
    public override IEnumerator Perform(CardGameObject _owner)
    {
        int _gold = int.Parse(m_Arguments[0]);

        int _goldOrigin = GameManager.Instance.GetPlayerGold(0);
        int _newGold = _goldOrigin + _gold;
        GameManager.Instance.SetPlayerGold(0, _newGold);
        
        Debug.Log($"CardOperation_AcquireGold::Perform(): gold changed. {_goldOrigin} -> {_newGold}");

        yield return new WaitForSeconds(1);
    }
}
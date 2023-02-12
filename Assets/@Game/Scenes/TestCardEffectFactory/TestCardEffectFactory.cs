using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardEffectFactory : MonoBehaviour
{
    [SerializeField] private Card _card;

    void Start()
    {
        string _content = "case:" +
                          "CardOperation_Test(1, 2, 3);" +
                          "CardOperation_Test(3, 2, 1);" +
                          "endcase" +
                          "case:" +
                          "CardOperation_Test(10, 20, 30);" +
                          "CardOperation_Test(30, 20, 10);" +
                          "endcase";
        var _cardEffect = CardEffectFactory.Create(_content);
        _card.StartCoroutine(_cardEffect.Perform(_card));
    }
}
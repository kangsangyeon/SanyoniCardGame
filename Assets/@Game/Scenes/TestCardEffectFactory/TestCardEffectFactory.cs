using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardEffectFactory : MonoBehaviour
{
    void Start()
    {
        string _content = "CardOperation_Test(1, 2, 3);" +
                          "CardOperation_Test(3, 2, 1);";
        CardEffectFactory.Create(_content);
    }
}
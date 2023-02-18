using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameObjectPool : ObjectPoolBase<CardGameObject>
{
    protected override void OnCreatePoolItem_Impl(CardGameObject _component)
    {
    }

    protected override void OnTakeFromPool_Impl(CardGameObject _component)
    {
    }

    protected override void OnReturnedToPool_Impl(CardGameObject _component)
    {
    }

    protected override void OnDestroyPoolObject_Impl(CardGameObject _component)
    {
    }
}
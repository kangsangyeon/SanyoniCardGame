using System;
using System.Collections.Generic;

public class CardEffectFactory
{
    public static CardEffectBase Create(string _type, List<string> _args)
    {
        Type _t = Type.GetType(_type);
        CardEffectBase _effect = Activator.CreateInstance(_t) as CardEffectBase;
        _effect.SetArguments(_args);
        return _effect;
    }
}
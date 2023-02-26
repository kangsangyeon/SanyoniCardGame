using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardEffectFactory
{
    private const string OperationTextRegex = @"^(?<OperationName>[0-9a-zA-Z]{1,})\((?<Params>[\w\W]+)\)$";

    public static CardEffect Create(string _content)
    {
        CardEffect _effect = new CardEffect();
        CardOperation_ChooseOne _chooseOne = null;

        while (_content.Length > 0)
        {
            _content.TrimStart();

            if (_content.StartsWith("case:"))
            {
                int _endCaseIndex = _content.IndexOf("endcase");
                int _caseContentCount = _endCaseIndex - 5;
                string _caseContent = _content.Substring(5, _caseContentCount); // "case:" 이후부터 "endcase" 이전의 문자열을 가져옵니다.

                if (_chooseOne == null)
                {
                    _chooseOne = ScriptableObject.CreateInstance<CardOperation_ChooseOne>();
                }

                CardEffect _caseEffect = Create(_caseContent);
                _chooseOne.m_EffectList.Add(_caseEffect);

                _content = _content.Substring(_endCaseIndex + 7); // "endcase" 이후의 내용을 읽습니다.
            }
            else
            {
                int _endIndex = _content.IndexOf(";");
                int _paramStartIndex = _content.IndexOf("(");
                int _paramEndIndex = _content.IndexOf(")");

                string _operationType = _content.Substring(0, _paramStartIndex);
                string _argString = _content.Substring(_paramStartIndex + 1, _paramEndIndex - _paramStartIndex - 1);
                List<string> _args = new List<string>(_argString.Split(",").Select(s => s.Trim()));

                var _operation = Create(_operationType, _args);
                _effect.m_OperationSequence.Add(_operation);

                _content = _content.Substring(_endIndex + 1);
            }
        }

        if (_chooseOne != null)
            _effect.m_OperationSequence.Add(_chooseOne);

        return _effect;
    }

    private static CardOperationBase Create(string _type, List<string> _args)
    {
        Type _t = Type.GetType(_type);
        CardOperationBase _operation = ScriptableObject.CreateInstance(_t) as CardOperationBase;
        _operation.SetArguments(_args);
        return _operation;
    }
}
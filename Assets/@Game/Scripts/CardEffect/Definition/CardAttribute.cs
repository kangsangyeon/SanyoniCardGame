using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SanyoniCardGame/Card")]
public class CardAttribute : ScriptableObject
{
    [SerializeField] private string m_CardName;
    [SerializeField] private Sprite m_Thumbnail;
    [SerializeField] private int m_Cost;
    [SerializeField] private int m_SuccessionPoint;
    [SerializeField] private int m_Link;
    [SerializeField] private ECardType m_Type;
    [SerializeField] private string m_Description;
    [SerializeField] private CardPrerequisite m_Prerequisite;
    [SerializeField] private List<CardOperationBase> m_OperationSequence;

    public string GetCardName() => m_CardName;
    public Sprite GetThumbnail() => m_Thumbnail;
    public int GetCost() => m_Cost;
    public int GetSuccessionPoint() => m_SuccessionPoint;
    public int GetLink() => m_Link;
    public ECardType GetType() => m_Type;
    public string GetDescription() => m_Description;
    public CardPrerequisite GetPrerequisite() => m_Prerequisite;
    public List<CardOperationBase> GetOperationSequence() => m_OperationSequence;
}
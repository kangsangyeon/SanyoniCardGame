using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SanyoniCardGame/Card")]
public class CardAttribute : ScriptableObject
{
    [SerializeField] private string m_Name;
    [SerializeField] private Texture2D m_Thumbnail;
    [SerializeField] private int m_Cost;
    [SerializeField] private ECardType m_Type;
    [SerializeField] private CardPrerequisite m_Prerequisite;
    [SerializeField] private List<CardOperationBase> m_OperationSequence;

    public string GetName() => m_Name;
    public Texture2D GetThumbnail() => m_Thumbnail;
    public int GetCost() => m_Cost;
    public ECardType GetType() => m_Type;
    public CardPrerequisite GetPrerequisite() => m_Prerequisite;
    public List<CardOperationBase> GetOperationSequence() => m_OperationSequence;
}
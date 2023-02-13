public enum EPlayerType
{
    Me,
    SpecificOpponent,
    AllOpponent,
    LeftOpponent,
    RightOpponent,
    Everyone
}

public enum EOrderedDummyType
{
    JunkDummy_Mine,
    JunkDummy_SpecificOpponent,
}

public enum ECardType
{
    Princess = 0x01,
    Action = 0x02,
    Attack = 0x04,
    Defense = 0x08,
    Territory = 0x10,
    Succession = 0x20,
    Calamity = 0x40,
    Subtype_Military = 0x80,
    Subtype_Magic = 0x100,
    Subtype_Maid = 0x200,
    Main = Princess | Action | Attack | Defense | Territory | Succession | Calamity,
    Subtype = Subtype_Military | Subtype_Magic | Subtype_Maid
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerType
{
    Guard,
    Thief
}

public class PlayerSettings
{
    public static string userName;

    // TODO: userType 서버로부터 받아와서 설정해줘야함
    public static EPlayerType userType = EPlayerType.Thief;
}

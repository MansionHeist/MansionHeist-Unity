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

    // TODO: userType �����κ��� �޾ƿͼ� �����������
    public static EPlayerType userType = EPlayerType.Thief;
}

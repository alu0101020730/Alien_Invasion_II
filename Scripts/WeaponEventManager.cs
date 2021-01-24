using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEventManager : MonoBehaviour
{
    public delegate void WeaponAction();
    public static event WeaponAction changeWeapon;
}

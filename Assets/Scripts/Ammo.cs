using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    AmmoSlot[] ammoSlots;

    //이 클래스의 내용물들을 전부 인스펙터에 보이게함
    [System.Serializable]
    //Ammo 클래스만 접근 가능
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    //변수가 private class 안에있기때문에 어떤 타입을 말하는 건지 알아야함. -> 메소드 전달값으로 AmmoType
    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }

    //탄약 슬롯과 탄약 종류가 서로 맞는지 확인
    //어떤 탄약 슬롯을 말하는지 모르기때문에 (ammotype)
    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        //특정 슬롯에 들어가는 탄약 종류를 말함
        foreach (AmmoSlot slot in ammoSlots)
        {
            //각 무기에 맞는 탄약 종류가 들어가도록 ammoType
            if (slot.ammoType == ammoType)
            {
                //특정 탄약 슬롯을 반환 
                return slot;
            }
        }
        //반환되는 값이 없으면 null반환
        return null;
    }
}

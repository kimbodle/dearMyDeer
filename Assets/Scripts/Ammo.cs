using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    AmmoSlot[] ammoSlots;

    //�� Ŭ������ ���빰���� ���� �ν����Ϳ� ���̰���
    [System.Serializable]
    //Ammo Ŭ������ ���� ����
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    //������ private class �ȿ��ֱ⶧���� � Ÿ���� ���ϴ� ���� �˾ƾ���. -> �޼ҵ� ���ް����� AmmoType
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

    //ź�� ���԰� ź�� ������ ���� �´��� Ȯ��
    //� ź�� ������ ���ϴ��� �𸣱⶧���� (ammotype)
    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        //Ư�� ���Կ� ���� ź�� ������ ����
        foreach (AmmoSlot slot in ammoSlots)
        {
            //�� ���⿡ �´� ź�� ������ ������ ammoType
            if (slot.ammoType == ammoType)
            {
                //Ư�� ź�� ������ ��ȯ 
                return slot;
            }
        }
        //��ȯ�Ǵ� ���� ������ null��ȯ
        return null;
    }
}

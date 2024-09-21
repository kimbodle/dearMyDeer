using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour, IInteractable
{
    [TextArea(3, 10)]
    [SerializeField] private string signText;
    public string GetInteractText()
    {
        return signText;
    }

    public bool RequiresInteraction()
    {
        return true; // ��ȣ�ۿ� �ʿ�
    }
}

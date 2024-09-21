using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour, IInteractable
{
    [TextArea(3, 10)]
    [SerializeField] private string signText;
    public string GetInteractText()
    {
        return signText;
    }
    public bool RequiresInteraction()
    {
        return false; // 상호작용 필요x
    }
}

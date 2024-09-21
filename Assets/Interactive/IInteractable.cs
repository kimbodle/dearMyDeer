public interface IInteractable
{
    string GetInteractText(); // 상호작용 시 반환할 텍스트
    bool RequiresInteraction(); // 상호작용 필요 여부
}

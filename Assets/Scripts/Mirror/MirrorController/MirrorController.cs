using UnityEngine;

public class MirrorController : MonoBehaviour
{
    private Animator _animator;
    private bool _isItemEquipped;

    private static readonly int EquipTrigger = Animator.StringToHash("Equip");
    private static readonly int UnequipTrigger = Animator.StringToHash("Unequip");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ToggleItem()
    {
        if (_isItemEquipped)
            UnequipItem();
        else
            EquipItem();

    }

    private void EquipItem()
    {
        _isItemEquipped = true;
        _animator.SetTrigger(EquipTrigger);
    }

    private void UnequipItem()
    {
        _isItemEquipped = false;
        _animator.SetTrigger(UnequipTrigger);
    }
}

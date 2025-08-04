using UnityEngine;

public class HolyWheats : MonoBehaviour , ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;
    public void Collect()
    {
        _playerController.SetJumpForce(_wheatDesignSO.IncreaseDecreaseMultiplier,_wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject); 
    }
}

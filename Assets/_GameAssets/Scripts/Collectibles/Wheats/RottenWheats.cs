using UnityEngine;

public class RottenWheats : MonoBehaviour , ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;


    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier,_wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject); 
    }
}

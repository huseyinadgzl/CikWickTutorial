using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO",menuName = "ScriptableObjects/WheatDesigSO")]
public class WheatDesingSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultiplier;
    [SerializeField] private float _resetBoostDuration;

    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
    public float ResetBoostDuration => _resetBoostDuration;
}

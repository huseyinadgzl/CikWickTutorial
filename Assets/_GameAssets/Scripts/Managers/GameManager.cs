using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Preferences")]
    [SerializeField] private EggCounterUI _eggCounterUI;

     [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;


    private int _currentEggCount;

    private void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;

        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);

        if (_currentEggCount == _maxEggCount)
        {
            //WİNN
            Debug.Log("Game Wİnnn?Ğ=??==?=");
            _eggCounterUI.SetEggCompleted();

        }
        Debug.Log("Egg Collected : " + _currentEggCount);
    }
}

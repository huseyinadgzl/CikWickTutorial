using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealthImage;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealthySprite;
    [SerializeField] private Sprite _playerUnHealthySprite;

    [Header("Settings")]
    [SerializeField] private float _scaleDuration;

    private RectTransform[] _playerHealthTransform;

    private void Awake()
    {
        _playerHealthTransform = new RectTransform[_playerHealthImage.Length];

        for (int i = 0; i < _playerHealthImage.Length; i++)
        {
            _playerHealthTransform[i] = _playerHealthImage[i].gameObject.GetComponent<RectTransform>();
        }
    }
    // FOR TEST
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageForAll();
         }
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImage.Length; i++)
        {
            if (_playerHealthImage[i].sprite == _playerHealthySprite)
            {
                AnimateDamageSprite(_playerHealthImage[i], _playerHealthTransform[i]);
                break;
            }
        }
    }

    public void AnimateDamageForAll()
    {
        for (int i = 0; i < _playerHealthImage.Length; i++)
        {
            AnimateDamageSprite(_playerHealthImage[i], _playerHealthTransform[i]);
        }
    }
    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnHealthySprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
}

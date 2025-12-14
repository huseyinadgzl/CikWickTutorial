using UnityEngine;

public class Eggs : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
        CameraShake.Instance.ShakeCamera(0.5f,0.5f);
        AudioManager.Instance.Play(SoundType.PickupGoodSound);

    }
}

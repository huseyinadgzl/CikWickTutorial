using UnityEngine;

public class Eggs : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
    }
}

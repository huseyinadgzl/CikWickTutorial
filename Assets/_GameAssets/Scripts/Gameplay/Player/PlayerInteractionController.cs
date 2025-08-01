using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.WheatTypes.GOLD_WHEAT))
        {
          other.gameObject.GetComponent<GoldenWheats>().Collect();
        }

        if (other.CompareTag(Consts.WheatTypes.HOLY_WHEAT))
        {
            other.gameObject.GetComponent<HolyWheats>().Collect();
        }
         
        if (other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT))
        {
            other.gameObject.GetComponent<RottenWheats>().Collect();
         }
    }
}

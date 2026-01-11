using UnityEngine;

public class Diamond : MonoBehaviour
{
   public int gems = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.AddGems(gems);
                Destroy(this.gameObject);
            }

        }
    }
}

using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * 3 * Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            IDamageable hit = collision.GetComponent<IDamageable>();
            if(hit != null)
            {
                hit.Damage();
                Destroy(this.gameObject);
            }

        }
    }
}

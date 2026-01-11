using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    private Spider _spider;
    void Start()
    {
        _spider = transform.parent.GetComponent<Spider>();
        
    }
    public void Fire()
    {
        Debug.Log("Spider Fire");
        _spider.Attack();
    }
}

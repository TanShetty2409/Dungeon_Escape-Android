using UnityEngine;

public class CastleTrigger : MonoBehaviour
{
    private bool _gameEnding = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_gameEnding)
            return;

        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.HasKeyToCastle)
            {
                _gameEnding = true;
                UIManager.Instance.FadeToBlackAndEnd();
            }
            else
            {
                UIManager.Instance.ShowNotification("The castle is locked. You need the key.");
            }
        }
    }
}

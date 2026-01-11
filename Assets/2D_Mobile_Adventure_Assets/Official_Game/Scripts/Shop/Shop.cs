using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
   [SerializeField] int currentItemSelected = -1;
    [SerializeField] int currentItemCost = -1;
    Player _player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _player = collision.GetComponent<Player>();
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }

            shopPanel.SetActive(true);

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }
    public void SelectedItem(int item)
    {
        Debug.Log("Item Selected" + item);
        currentItemSelected = item;
        switch (item)
        {
            case 0:
                currentItemCost = 200;
                UIManager.Instance.UpdateItemSelected(81);
                break;
            case 1:
                currentItemCost = 400;
                UIManager.Instance.UpdateItemSelected(-29);
                break;
            case 2:
                currentItemCost = 100;
                UIManager.Instance.UpdateItemSelected(-139);
                break;
        }
    }
    public void BuyItem()
    {
        if (_player == null) return;

        if (currentItemSelected == -1 || currentItemCost <= 0)
        {
            UIManager.Instance.ShowNotification("Select an item first");
            return;
        }

        if (_player.diamonds < currentItemCost)
        {
            UIManager.Instance.ShowNotification("Not enough diamonds!");
            shopPanel.SetActive(false);
            return;
        }

        else if (currentItemSelected == 0)
        {
            if (GameManager.Instance.hasFlameSword)
            {
                UIManager.Instance.ShowNotification("Already Purchased!");
                return;
            }

            GameManager.Instance.hasFlameSword = true;
            UIManager.Instance.ShowNotification("Flame Sword Purchased");
        }

        else if (currentItemSelected == 2)
        {
            if (GameManager.Instance.HasKeyToCastle)
            {
                UIManager.Instance.ShowNotification("Already Purchased!");
                return;
            }

            GameManager.Instance.HasKeyToCastle = true;
            UIManager.Instance.ShowNotification("Castle Key Purchased");

        }

        _player.diamonds -= currentItemCost;
        shopPanel.SetActive(false);
    }
 
}

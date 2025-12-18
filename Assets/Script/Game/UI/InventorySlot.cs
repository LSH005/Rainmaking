using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image BG;
    public Image itemIcon;
    public TextMeshProUGUI itemAmount;
    public ItemType currentBlockType;
    public bool isEmptySlot = true;

    int count = 0;

    void Start()
    {
        itemIcon.sprite = InventoryManager.instance.airImage;
        ClearSlot();
        itemAmount.gameObject.SetActive(false);
    }

    public void SetBlockType(ItemType type)
    {
        currentBlockType = type;

        if (type == ItemType.Air)
        {
            itemIcon.sprite = InventoryManager.instance.airImage;
        }
    }

    public void AddItemCount(int amount)
    {
        count += amount;
        if (count <= 0)
        {
            ClearSlot();
            return;
        }
        else
        {
            itemAmount.gameObject.SetActive(true);
            isEmptySlot = false;
        }


        itemAmount.text = count.ToString();
    }

    public void SetItemCount(int number)
    {
        if (number <= 0)
        {
            ClearSlot();
            return;
        }
        else
        {
            itemAmount.gameObject.SetActive(true);
            isEmptySlot = false;
        }

        count = number;
        itemAmount.text = count.ToString();
    }


    public void ClearSlot()
    {
        count = 0;
        itemAmount.gameObject.SetActive(false);
        SetBlockType(ItemType.Air);
        isEmptySlot = true;
    }

    public void SetIcon(Sprite icon)
    {
        itemIcon.sprite = icon;
    }

    public void SetColor(Color targetColor)
    {
        BG.color = targetColor;
    }
}
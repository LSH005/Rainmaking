using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public int ButtonNumber = 1;
    public Sprite emptySprite;
    [HideInInspector] public CraftingManager manager;
    Image image;
    Sprite blockSprite;
    bool hasBlock;
    int binaryButtonID;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        binaryButtonID = (int)Mathf.Pow(2, ButtonNumber);
        blockSprite = InventoryManager.instance.blockImage;
        ClearSlot();
    }

    public void ClearSlot()
    {
        hasBlock = false;
        image.sprite = emptySprite;
    }

    public void ToggleSlot()
    {
        if (!hasBlock)
        {
            InventorySlot slot = InventoryManager.instance.FindSlot(ItemType.Block);
            if (slot == null) return;
            if (slot.isEmptySlot) return;
            slot.AddItemCount(-1);
        }
        else
        {
            InventoryManager.instance.AddItem(ItemType.Block, 1);
        }

        hasBlock = !hasBlock;

        if (hasBlock)
        {
            image.sprite = blockSprite;
            manager.AddCraftId(binaryButtonID);
        }
        else
        {
            image.sprite = emptySprite;
            manager.AddCraftId(-binaryButtonID);
        }
    }
}

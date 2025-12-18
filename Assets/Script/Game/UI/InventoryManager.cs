using UnityEngine;

public enum ItemType { Block, Pull, Air };

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    public InventorySlot[] slots;

    [Header("빈찬합")]
    public Sprite airImage;
    [Header("아이템")]
    public Sprite blockImage;
    public Sprite pullImage;

    private int selectedIndex = -1;
    bool hasSelectedSlot = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ClearSelect();
    }

    private void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetSelectedIndex(i);
            }
        }

        if (Test.Instance.isTest)
        {
            if (Input.GetKey(KeyCode.Keypad1))
            {
                AddItem(ItemType.Block, 5);
            }
        }
    }

    void SetSelectedIndex(int index)
    {
        if (index != selectedIndex)
        {
            selectedIndex = index;
            hasSelectedSlot = true;

            foreach (InventorySlot slot in slots)
            {
                slot.SetColor(Color.gray);
            }

            slots[index].SetColor(Color.red);
        }
        else
        {
            ClearSelect();
        }
    }

    public InventorySlot GetSelectedInventorySlot()
    {
        if (selectedIndex >= 0) return slots[selectedIndex];
        else return null;
    }

    void ClearSelect()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.SetColor(Color.gray);
        }

        hasSelectedSlot = false;
        selectedIndex = -1;
    }

    public void AddItem(ItemType blockType, int itemCount)
    {
        InventorySlot slot = FindSlot(blockType);

        if (slot != null)
        {
            if (slot.isEmptySlot)
            {
                slot.SetBlockType(blockType);
                slot.AddItemCount(itemCount);
                slot.SetIcon(GetImageFromBlockType(blockType));
            }
            else
            {
                slot.AddItemCount(itemCount);
            }
        }
    }

    public void ShowInventory(bool show)
    {
        foreach (var slot in slots)
        {
            slot.gameObject.SetActive(show);
        }
    }

    public InventorySlot FindSlot(ItemType type)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];

            if (slot.isEmptySlot) continue;

            if (slot.currentBlockType == type)
            {
                return slot;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];

            if (slot.isEmptySlot)
            {
                return slot;
            }
        }

        return null;
    }

    public Sprite GetImageFromBlockType(ItemType type)
    {
        switch (type)
        {
            case ItemType.Block:
                return blockImage;
            case ItemType.Pull:
                return pullImage;
        }

        return airImage;
    }

    public ItemType GetSelectedItem()
    {
        if (hasSelectedSlot)
        {
            InventorySlot slot = slots[selectedIndex];
            if (slot != null)
            {
                return slot.currentBlockType;
            }
        }
        
        return ItemType.Air;
    }
}
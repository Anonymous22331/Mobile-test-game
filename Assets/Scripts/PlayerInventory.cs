using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInventory : MonoBehaviour
{
    public Button buttonPrefab;
    private GameObject closestItem;
    private float distanceToClosestItem = Mathf.Infinity;
    public List<GameObject> inventoryItems = new List<GameObject>();
    public List<int> inventoryItemsCount = new List<int>();
    private int indexOfItem;
    private GameObject[] slots;
    private int currentIndex;
    private bool deleteButtonActive = false; 

    private void Awake()
    {
        slots = GameObject.FindGameObjectsWithTag("Slot");
    }

    private GameObject FindClosestItem()
    {
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject currentItem in allItems)
        {
            float distanceToItem = (currentItem.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToItem < distanceToClosestItem)
            {
                distanceToClosestItem = distanceToItem;
                closestItem = currentItem;
            }
        }
        return closestItem;
    }

    int GameObjectIndex(GameObject target)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].name == target.name)
            {
                return i;
            }
        }
        return -1;
    }

    private void AddItem()
    {
        FindClosestItem();
        if (closestItem != null)
        {
            if (Vector3.Distance(transform.GetChild(1).transform.position, closestItem.transform.position) < 0.5f)
            {
                if (GameObjectIndex(closestItem) != -1) // If item exist in inventory
                {
                    indexOfItem = GameObjectIndex(closestItem);
                    inventoryItemsCount[indexOfItem] += 1;
                    slots[indexOfItem].transform.GetChild(1).GetComponent<Image>().sprite = closestItem.GetComponent<SpriteRenderer>().sprite;
                    slots[indexOfItem].transform.GetChild(0).GetComponent<Text>().text = inventoryItemsCount[indexOfItem].ToString();
                    Destroy(closestItem);
                }
                else
                {
                    if (inventoryItems.Count < 3) // If item is not in inventory and inventory isnt full
                    {
                        inventoryItems.Add(closestItem);
                        inventoryItemsCount.Add(1);
                        slots[inventoryItems.Count - 1].transform.GetChild(1).GetComponent<Image>().sprite = closestItem.GetComponent<SpriteRenderer>().sprite;
                        slots[inventoryItems.Count - 1].transform.GetChild(0).GetComponent<Text>().text = "";
                        closestItem.transform.position = closestItem.transform.position + new Vector3(1000, 1000, 0);
                    }
                    else
                    {
                        // Если инвентарь переполнен
                    }
                }
            }
        }
    }

    public void CreateDeleteButton()
    {
        var currentSlot = EventSystem.current.currentSelectedGameObject;
        currentIndex = System.Convert.ToInt32(currentSlot.name.Substring(currentSlot.name.Length - 1)) - 1;
        if (inventoryItemsCount.Count - 1 >= indexOfItem)
        {
            if (deleteButtonActive)
            {
                ToggleButton(false);
            }
            else
            {
                ToggleButton(true);
            }
        }
    }

    private void ToggleButton(bool toggle)
    {
        var currentSlot = EventSystem.current.currentSelectedGameObject;
        currentSlot.transform.GetChild(2).GetComponent<Image>().enabled = toggle;
        currentSlot.transform.GetChild(2).GetComponent<Button>().enabled = toggle;
        currentSlot.transform.GetChild(2).GetChild(0).GetComponent<Text>().enabled = toggle;
        deleteButtonActive = toggle;
    }

    private void DeleteButtonClose()
    {
        var currentSlot = EventSystem.current.currentSelectedGameObject.transform.parent;
        currentSlot.transform.GetChild(2).GetComponent<Image>().enabled = false;
        currentSlot.transform.GetChild(2).GetComponent<Button>().enabled = false;
        currentSlot.transform.GetChild(2).GetChild(0).GetComponent<Text>().enabled = false;
    }

    public void DeleteItem()
    {
        var currentSlot = EventSystem.current.currentSelectedGameObject.transform.parent;
        currentSlot.transform.GetChild(1).GetComponent<Image>().sprite = null;
        currentSlot.transform.GetChild(0).GetComponent<Text>().text = "Empty";
        inventoryItemsCount.RemoveAt(currentIndex);
        Destroy(inventoryItems[currentIndex]);
        inventoryItems.RemoveAt(currentIndex);
        DeleteButtonClose();
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        if (inventoryItemsCount.Count >= 1)
        {
            slots[0].transform.GetChild(1).GetComponent<Image>().sprite = slots[1].transform.GetChild(1).GetComponent<Image>().sprite;
            slots[0].transform.GetChild(0).GetComponent<Text>().text = slots[1].transform.GetChild(0).GetComponent<Text>().text;
            slots[1].transform.GetChild(1).GetComponent<Image>().sprite = null;
            slots[1].transform.GetChild(0).GetComponent<Text>().text = "Empty";
            if (inventoryItemsCount.Count == 2)
            {
                slots[1].transform.GetChild(1).GetComponent<Image>().sprite = slots[2].transform.GetChild(1).GetComponent<Image>().sprite;
                slots[1].transform.GetChild(0).GetComponent<Text>().text = slots[2].transform.GetChild(0).GetComponent<Text>().text;
                slots[2].transform.GetChild(1).GetComponent<Image>().sprite = null;
                slots[2].transform.GetChild(0).GetComponent<Text>().text = "Empty";
            }
        }
        
    }

    void Update()
    {
        AddItem();
    }
}

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SaveGame : MonoBehaviour
{
    private bool toggle;

    void Start()
    {
        toggle = false;
    }

    private void ToggleButtons(bool toggle)
    {
        var menu = EventSystem.current.currentSelectedGameObject;
        menu.transform.GetChild(1).GetComponent<Image>().enabled = toggle;
        menu.transform.GetChild(2).GetComponent<Image>().enabled = toggle;
        menu.transform.GetChild(3).GetComponent<Image>().enabled = toggle;
        menu.transform.GetChild(1).GetComponent<Button>().enabled = toggle;
        menu.transform.GetChild(2).GetComponent<Button>().enabled = toggle;
        menu.transform.GetChild(3).GetComponent<Button>().enabled = toggle;
        menu.transform.GetChild(1).GetChild(0).GetComponent<Text>().enabled = toggle;
        menu.transform.GetChild(2).GetChild(0).GetComponent<Text>().enabled = toggle;
        menu.transform.GetChild(3).GetChild(0).GetComponent<Text>().enabled = toggle;
    }

    public void OpenMenu()
    {
        ToggleButtons(toggle);
        toggle = !toggle;
        Debug.Log(toggle);
    }

    public void SaveM()
    {
        var instance = new SaveData();
        instance.Save();
    }
    
    public void LoadM()
    {
        var instance = new SaveData();
        instance.Load();
    }

    public void ResetM()
    {
        var instance = new SaveData();
        instance.ResetData();
    }
}

[Serializable]
class SaveData
{
    public int ammoCount;
    public List<float> playerPosition;
    //public List<int> itemsCount;

    public void SetParams()
    {
        ammoCount = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerAttackAndHealth>().ammoCount;
        var VectorPlayerCoords = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        playerPosition = new List<float>() { VectorPlayerCoords.x, VectorPlayerCoords.y, VectorPlayerCoords.z };
        //itemsCount = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInventory>().inventoryItemsCount;
    }

    public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/SaveFile1.dat");
            SaveData data = new SaveData();
            SetParams();
            data.ammoCount = ammoCount;
            data.playerPosition = playerPosition;
            //data.itemsCount = itemsCount;
            bf.Serialize(file, data);
            file.Close();
            Debug.Log("Saved!");
        }

    public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/SaveFile1.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file =
                File.Open(Application.persistentDataPath + "/SaveFile1.dat", FileMode.Open);
                SaveData data = (SaveData)bf.Deserialize(file);
                file.Close();
                GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerAttackAndHealth>().ammoCount = data.ammoCount;
                GameObject.FindGameObjectsWithTag("Player")[0].transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
                //GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInventory>().inventoryItemsCount = data.itemsCount;
                Debug.Log("Game data loaded!");
            }
            else
                Debug.LogError("Error! No such save file");
        }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFile1.dat"))
        {
            File.Delete(Application.persistentDataPath + "/SaveFile1.dat");
             GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerAttackAndHealth>().ammoCount = 10;
             GameObject.FindGameObjectsWithTag("Player")[0].transform.position = new Vector3(-5.3f,0,0);
             //GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInventory>().inventoryItemsCount = new List<int>();
             Debug.Log("Data deleted!");
        }
        else
            Debug.LogError("Error! No such file");
    }
}



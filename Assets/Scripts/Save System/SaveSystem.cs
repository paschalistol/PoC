﻿using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    public bool died;

    [System.Serializable]
    public class SaveData
    {
        public List<InteractablesData> interactablesData = new List<InteractablesData>();
        public List<EnemyData> enemiesData = new List<EnemyData>();
        public PlayerData playerData;
        public bool alarmOn;
        public float highScore;
        public int deathCounter;

    }

    #region Datatypes

    [System.Serializable]
    public abstract class Data
    {
        public float[] position;
        public float[] rotation;
    }

    [System.Serializable]
    public class PlayerData : Data
    {

        public int itemHoldIndex;

        public PlayerData(GameObject player)
        {
            position = new float[3];
            position[0] = player.transform.position.x;
            position[1] = player.transform.position.y;
            position[2] = player.transform.position.z;

            rotation = new float[3];
            rotation[0] = player.transform.eulerAngles.x;
            rotation[1] = player.transform.eulerAngles.y;
            rotation[2] = player.transform.eulerAngles.z;
        }
    }

    [System.Serializable]
    public class EnemyData : Data
    {

        public EnemyData(GameObject enemy)
        {
            position = new float[3];
            position[0] = enemy.transform.position.x;
            position[1] = enemy.transform.position.y;
            position[2] = enemy.transform.position.z;

            rotation = new float[3];
            rotation[0] = enemy.transform.eulerAngles.x;
            rotation[1] = enemy.transform.eulerAngles.y;
            rotation[2] = enemy.transform.eulerAngles.z;
        }
    }


    [System.Serializable]
    public class InteractablesData : Data
    {

        public InteractablesData(GameObject interactables)
        {
            position = new float[3];
            position[0] = interactables.transform.position.x;
            position[1] = interactables.transform.position.y;
            position[2] = interactables.transform.position.z;

            rotation = new float[3];
            rotation[0] = interactables.transform.eulerAngles.x;
            rotation[1] = interactables.transform.eulerAngles.y;
            rotation[2] = interactables.transform.eulerAngles.z;
        }
    }

    [System.Serializable]
    public class DoorsData : InteractablesData
    {
        public bool used;
        public DoorsData(GameObject door, bool used) : base(door)
        {
            this.used = used;
        }
    }

    [System.Serializable]
    public class ValuablesData : InteractablesData
    {
        public bool taken;
        public ValuablesData(GameObject valuable, bool taken) : base(valuable)
        {
            this.taken = taken;
        }
    }
    #endregion  


    #region Save

    public void Save()
    {
        SaveData saveData = new SaveData();

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Save.oof";
        FileStream stream = new FileStream(path, FileMode.Create);


        SavingPlayer(saveData);
        SavingInteractables(saveData);
        SavingEnemies(saveData);
        SavingAlarm(saveData);
        SavingPlayerPrefs(saveData);
        

        formatter.Serialize(stream, saveData);
        stream.Close();

        Debug.Log("saved");
    }

    private void SavingPlayer(SaveData saveData)
    {
        GameObject player = GameManager.gameManager.player;
        PlayerData playerData = new PlayerData(player);

        for (int i = 0; i < GameManager.gameManager.interactables.Count; i++)
        {
            GameObject item = GameManager.gameManager.interactables[i];
            CharacterHoldItemStateMachine holdItemScript = player.GetComponent<CharacterHoldItemStateMachine>();
            if (holdItemScript.ObjectCarried != null && item == holdItemScript.ObjectCarried)
            {
                playerData.itemHoldIndex = i;
                break;
            }
            else
            {
                playerData.itemHoldIndex = -1;
            }
        }

        saveData.playerData = playerData;
    }

    private void SavingInteractables(SaveData saveData)
    {
        for (int i = 0; i < GameManager.gameManager.interactables.Count; i++)
        {
            GameObject interactable = GameManager.gameManager.interactables[i];

            if (interactable.layer == 9)
            {
                bool used = interactable.transform.GetChild(0).GetComponent<Door>().used;
                DoorsData doorsData = new DoorsData(interactable, used);
                saveData.interactablesData.Add(doorsData);
            }else if (interactable.layer == 24)
            {
                bool taken = interactable.activeInHierarchy;
                Debug.Log(taken);
                ValuablesData valuablesData = new ValuablesData(interactable, taken);
                saveData.interactablesData.Add(valuablesData);
            }
            else
            {
                InteractablesData interactableData = new InteractablesData(interactable);
                saveData.interactablesData.Add(interactableData);
            }

        }
    }

    private void SavingEnemies(SaveData saveData)
    {
        for (int i = 0; i < GameManager.gameManager.enemies.Count; i++)
        {
            GameObject enemy = GameManager.gameManager.enemies[i];
            EnemyData enemyData = new EnemyData(enemy);
            saveData.enemiesData.Add(enemyData);
        }
    }

    private void SavingAlarm(SaveData saveData)
    {
        saveData.alarmOn = GameController.activatedAlarm;
    }

    private void SavingPlayerPrefs(SaveData saveData)
    {
        saveData.highScore = PlayerPrefs.GetFloat("Highscore");
        saveData.deathCounter = PlayerPrefs.GetInt("deathCounter");
    }


    #endregion


    #region Load

    public void Load()
    {
        string path = Application.persistentDataPath + "/Save.oof";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            Debug.Log(path);
            LoadPlayer(saveData);
            LoadInteractables(saveData);
            LoadEnemies(saveData);
            LoadAlarm(saveData);
            if (died != true)
            {
                LoadPlayerPrefs(saveData);

            }

            stream.Close();


        }
        else
        {

        }
        died = false;

    }

    #region Loading Player
    private void LoadPlayer(SaveData saveData)
    {
        GameObject player = GameManager.gameManager.player;
        player.transform.position = LoadPlayerPosition(saveData);
        player.transform.eulerAngles = LoadPlayerRotation(saveData);


        /* CharacterHoldItemStateMachine holdItemMachine = player.GetComponent<CharacterHoldItemStateMachine>();

         if (saveData.playerData.itemHoldIndex >= 0)
         {
             GameObject itemHolding = GameManager.gameManager.interactables[saveData.playerData.itemHoldIndex];
             Debug.Log(saveData.playerData.itemHoldIndex);
             holdItemMachine.ObjectCarried = itemHolding;
             holdItemMachine.holdingSth = true;
             holdItemMachine.ChangeState<HoldingItem>();

             Interactable interactable = itemHolding.GetComponent<Interactable>();
             interactable.StartInteraction();

         }*/


    }

    private Vector3 LoadPlayerPosition(SaveData saveData)
    {
        float[] position = saveData.playerData.position;
        float x = position[0];
        float y = position[1];
        float z = position[2];
        return new Vector3(x, y, z);
    }

    private Vector3 LoadPlayerRotation(SaveData saveData)
    {
        float[] rotation = saveData.playerData.rotation;
        float x = rotation[0];
        float y = rotation[1];
        float z = rotation[2];
        return new Vector3(x, y, z);
    }

    #endregion

    #region Loading Interactables
    private void LoadInteractables(SaveData saveData)
    {
        for (int i = 0; i < saveData.interactablesData.Count; i++)
        {
            GameObject interactable = GameManager.gameManager.interactables[i];
            interactable.transform.position = LoadInteractablesPosition(saveData, i);
            interactable.transform.eulerAngles = LoadInteractablesRotation(saveData, i);

            if (interactable.layer == 9)
            {
                DoorsData doorData = (DoorsData)saveData.interactablesData[i];
                interactable.transform.GetChild(0).GetComponent<Door>().used = doorData.used;
            }else if (interactable.layer == 24 && died != true)
            {
                ValuablesData valuableData = (ValuablesData)saveData.interactablesData[i];
                if(valuableData.taken == true)
                {
                    interactable.SetActive(true);
                }
            }

        }
    }

    private Vector3 LoadInteractablesPosition(SaveData saveData, int i)
    {
        float[] position = saveData.interactablesData[i].position;
        float x = position[0];
        float y = position[1];
        float z = position[2];
        return new Vector3(x, y, z);
    }

    private Vector3 LoadInteractablesRotation(SaveData saveData, int i)
    {
        float[] rotation = saveData.interactablesData[i].rotation;
        float x = rotation[0];
        float y = rotation[1];
        float z = rotation[2];

        return new Vector3(x, y, z);

    }

    #endregion

    #region Load enemies

    private void LoadEnemies(SaveData saveData)
    {
        for (int i = 0; i < saveData.enemiesData.Count; i++)
        {
            GameManager.gameManager.enemies[i].transform.rotation = LoadEnemiesRotation(saveData, i);
            GameManager.gameManager.enemies[i].transform.position = LoadEnemiesPosition(saveData, i);

        }
    }

    private Vector3 LoadEnemiesPosition(SaveData saveData, int i)
    {
        float[] position = saveData.enemiesData[i].position;
        float x = position[0];
        float y = position[1];
        float z = position[2];
        return new Vector3(x, y, z);
    }

    private Quaternion LoadEnemiesRotation(SaveData saveData, int i)
    {
        float[] rotation = saveData.enemiesData[i].rotation;
        float x = rotation[0];
        float y = rotation[1];
        float z = rotation[2];
        return new Quaternion(0, x, y, z);
    }
    #endregion


    private void LoadAlarm(SaveData saveData)
    {
        GameController.activatedAlarm = saveData.alarmOn;

        GameObject.Find("GameController (1)").GetComponent<GameController>().BlinkingTint();


    }

    private void LoadPlayerPrefs(SaveData saveData)
    {
        PlayerPrefs.SetFloat("Highscore", saveData.highScore);
        PlayerPrefs.SetInt("deathCounter", saveData.deathCounter);
    }

    #endregion




}

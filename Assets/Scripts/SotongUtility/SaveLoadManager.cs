using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager{
    
    public static void SaveData<T> (T rawData, string path, string fileName) {
        BinaryFormatter bf = new BinaryFormatter ();
        FileStream file = File.Create (path + "/" + fileName);

        T data = rawData;

        bf.Serialize (file, data);
        file.Close ();
    }
    public static void LoadData<T> (string path, string fileName, out T targetData) {
        targetData = default(T);

        if (File.Exists (path + "/" + fileName)) {
            BinaryFormatter bf = new BinaryFormatter ();
            FileStream file = File.Open (path + "/" + fileName, FileMode.Open);
            T data = (T) bf.Deserialize (file);
            file.Close ();

            targetData = data;
        }

    }

    public static void SaveData<T> (List<T> rawData, string path, string fileName) {
        BinaryFormatter bf = new BinaryFormatter ();
        FileStream file = File.Create (path + "/" + fileName);

        List<T> data = rawData;

        bf.Serialize (file, data);
        file.Close ();
    }
    public static void LoadData<T> (string path, string fileName, out List<T> targetData) {
        targetData = new List<T>();

        if (File.Exists (path + "/" + fileName)) {
            BinaryFormatter bf = new BinaryFormatter ();
            FileStream file = File.Open (path + "/" + fileName, FileMode.Open);
            List<T> data = (List<T>) bf.Deserialize (file);
            file.Close ();

            targetData = data;
        }

    }
}
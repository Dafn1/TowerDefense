using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    [Serializable]
    internal class Saver<T>
    {
       
        public static void TryLoad(string fileName, ref T data)
        {
            var path = FileHendler.Path(fileName);
            if (File.Exists(path))
            {
                
                var dataString=File.ReadAllText(path);
                var saver=JsonUtility.FromJson<Saver<T>>(dataString);
                data= saver.data;
            }
            else
            {
                Debug.Log($"No file at {path}");
            }
        }

        public static void Save(string fileName, T data)
        {
            var wrappes = new Saver<T> { data = data };
            var dataString = JsonUtility.ToJson(wrappes);

            File.WriteAllText(FileHendler.Path(fileName), dataString);
        }

        

        public T data;
    }
    public static class FileHendler
    {
        public static string Path(string fileName)
        {
            return $"{Application.persistentDataPath}/{fileName}";
        }
        public static void Reset(string fileName)
        {
            var path = Path(fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        internal static bool HasFile(string fileName)
        {

            return File.Exists(Path(fileName));
        }
    }
}
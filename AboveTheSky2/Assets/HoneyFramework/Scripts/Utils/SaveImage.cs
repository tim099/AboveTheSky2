using UnityEngine;
using System.Collections;
using System.IO;

namespace HoneyFramework
{
   
    public class SaveImage
    {
        static public void SavePNG(Texture2D texture, string name, string dir)
        {
            if (texture == null) return;

            byte[] data = texture.EncodeToPNG();
            string dirPath = Application.dataPath + "/../" + dir;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            FileStream file = File.Open(dirPath + "/" + name + @".png", FileMode.OpenOrCreate);
            BinaryWriter binary = new BinaryWriter(file);
            binary.Write(data);
            file.Close();
        }

        static public void SaveJPG(Texture2D texture, string name, string dir)
        {
            if (texture == null) return;

            byte[] data = texture.EncodeToJPG();
            string dirPath = Application.dataPath + "/../" + dir;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            FileStream file = File.Open(dirPath + "/" + name + @".jpg", FileMode.OpenOrCreate);
            BinaryWriter binary = new BinaryWriter(file);
            binary.Write(data);
            file.Close();
        }
    }
}
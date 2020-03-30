using UnityEngine;
using System.IO;

public class SaveSystem
{
    public static readonly string SAVE_FOLDER = "D:/Datazzz/Unity Projects/Personal/SKIDOS Assignment/SkidosAssignment_Project_1/Assets/Json Files";
    
    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static string Load()
    {
        if (File.Exists(SAVE_FOLDER + "/save.json"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    private static string GetAndroidExternalFilesDir()
{
     using (AndroidJavaClass unityPlayer = 
            new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
     {
          using (AndroidJavaObject context = 
                 unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
          {
               // Get all available external file directories (emulated and sdCards)
               AndroidJavaObject[] externalFilesDirectories = 
                                   context.Call<AndroidJavaObject[]>
                                   ("getExternalFilesDirs", (object)null);
               AndroidJavaObject emulated = null;
               AndroidJavaObject sdCard = null;
               for (int i = 0; i < externalFilesDirectories.Length; i++)
               {
                    AndroidJavaObject directory = externalFilesDirectories[i];
                    using (AndroidJavaClass environment = 
                           new AndroidJavaClass("android.os.Environment"))
                    {
                        // Check which one is the emulated and which the sdCard.
                        bool isRemovable = environment.CallStatic<bool>
                                          ("isExternalStorageRemovable", directory);
                        bool isEmulated = environment.CallStatic<bool>
                                          ("isExternalStorageEmulated", directory);
                        if (isEmulated)
                            emulated = directory;
                        else if (isRemovable && isEmulated == false)
                            sdCard = directory;
                    }
               }
               // Return the sdCard if available
               if (sdCard != null)
                    return sdCard.Call<string>("getAbsolutePath");
               else
                    return emulated.Call<string>("getAbsolutePath");
            }
      }
}
}
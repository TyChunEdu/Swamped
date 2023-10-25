using System;
using UnityEngine;

namespace Utils
{
   public static class Utils
   {
      public static T[] GetAllScriptableObjects<T>(string directory) where T : ScriptableObject
      {
         var allObjects = Resources.LoadAll(directory);
         return Array.ConvertAll(allObjects, i => (T)i);
      }

      public static T RandomChoice<T>(T[] array)
      {
         return array[UnityEngine.Random.Range(0, array.Length)];
      }
   }
}
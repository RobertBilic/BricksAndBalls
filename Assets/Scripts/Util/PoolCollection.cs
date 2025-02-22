using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCollection 
{
   private static PoolCollection _instance;
   public static PoolCollection Instance
   {
      get
      {
         if(_instance == null)
         {
            _instance = new PoolCollection();
         }
         return _instance;
      }
   }

   public PoolCollection()
   {
      poolDictionary = new Dictionary<Type, object>();
   }

   Dictionary<Type, object> poolDictionary;

   public Pool<T> GetPool<T>() where T : MonoBehaviour
   {
      var type = typeof(T);
      if(poolDictionary.ContainsKey(type))
         return poolDictionary[type] as Pool<T>;

      var newPool = new Pool<T>();
      poolDictionary.Add(type, newPool);
      return newPool;
   }
}

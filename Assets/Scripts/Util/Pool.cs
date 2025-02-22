using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
   public Pool()
   {
      pooledObjects = new Queue<T>();
   }

   private Queue<T> pooledObjects;

   private void AddToPool(T obj)
   {
      pooledObjects.Enqueue(obj);
      obj.gameObject.SetActive(false);
   }

   private T TakeFromPool()
   {
      if(pooledObjects.Count == 0)
         return null;

      var obj = pooledObjects.Dequeue();
      obj.gameObject.SetActive(true);
      return obj;
   }

   public bool HasItems() => pooledObjects.Count != 0;

   private void Destroy()
   {
      foreach(var obj in pooledObjects)
         GameObject.Destroy(obj.gameObject);
   }
}

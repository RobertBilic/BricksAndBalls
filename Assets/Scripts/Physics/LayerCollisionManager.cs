
using System.Collections.Generic;

public class LayerCollisionManager
{
   Dictionary<int, int> layerLayerMaskDict = new Dictionary<int, int>();

   public bool CanCollide(int id1, int id2)
   {
      return (layerLayerMaskDict[id1] & id2) != 0 && (layerLayerMaskDict[id2] & id1) != 0;
   }

   public void AddCollisionRule(int id1, int id2)
   {
      if(!IsPowerOfTwo(id1) || !IsPowerOfTwo(id2))
         throw new System.Exception("Invalid layer index");

      if(!layerLayerMaskDict.ContainsKey(id1))
      {
         layerLayerMaskDict.Add(id1, id2);
      } else
      {
         layerLayerMaskDict[id1] = layerLayerMaskDict[id1] | id2;
      }

      if(!layerLayerMaskDict.ContainsKey(id2))
      {
         layerLayerMaskDict.Add(id2, id1);
      } else
      {
         layerLayerMaskDict[id2] = layerLayerMaskDict[id2] | id1;
      }
   }

   public void RemoveCollisionRule(int id1, int id2)
   {
      if(!IsPowerOfTwo(id1) || !IsPowerOfTwo(id2))
         throw new System.Exception("Invalid layer index");

      if(!layerLayerMaskDict.ContainsKey(id1) || !layerLayerMaskDict.ContainsKey(id2))
         return;

      layerLayerMaskDict[id1] = layerLayerMaskDict[id1] & id2;
      layerLayerMaskDict[id2] = layerLayerMaskDict[id2] & id1;
   }

   private bool IsPowerOfTwo(int number)
   {
      return number > 0 && (number & (number - 1)) == 0;
   }
}
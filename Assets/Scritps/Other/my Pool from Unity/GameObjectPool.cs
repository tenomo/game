using System.Collections.Generic;
using UnityEngine;

namespace   ObjectPool
{
    [System.Serializable]
    public class GameObjectPool
    {
        private List<Prefab> Pool = new List<Prefab>();

        /// <summary>
        /// Содержит ссылку на префаб.
        /// </summary>
        public Prefab prefab;
        /// <summary>
        /// Количество игровых объектов в пуле.
        /// </summary>
        public byte Count;

        /// <summary>
        /// Возвращает игровой объект.
        /// </summary>
        /// <returns></returns>
        public GameObject GetGameObject()
        {
            return this.GetNotUsedObject().gameObject;
        }

        /// <summary>
        /// Находет в пуле неиспользуемый обьекта. Если обьект не найден или все 
        /// объкты используються, добавляет новый объект в пул.
        /// </summary>
        /// <returns></returns>
        private Prefab GetNotUsedObject()
        {
            foreach (var item in Pool)
            {
                if (Pool.Exists(x => x.isUsed == false))
                {
                    Prefab p = Pool.Find(x => x.isUsed == false);
                    p.isUsed = true;
                    return p;
                }
                else {
                    Pool.Add(prefab.Clone());
                    return GetNotUsedObject();
                }
            }
            throw new UnityException();
        }

        /// <summary>
        /// Инициализация пула. Необхолимо вызывать в Start или Awake. 
        /// </summary>
        public void InitializePool()
        {
            try
            {
                if (Count > 1)
                    this.prefab.CloneByCount(this.Pool, this.Count);
                else
                    prefab.Clone();
            }
            catch (UnityException ex)
            {
                Debug.LogError("Невозможно инициализировать пул. " + ex.ToString());
            }
        }
    }
}

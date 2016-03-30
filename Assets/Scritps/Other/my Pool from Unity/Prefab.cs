using UnityEngine;

namespace   ObjectPool
{
    /// <summary>
    /// Выполняет роль оболочки, которая управляет состоянием игрового объекта в пуле.
    /// </summary>
    [System.Serializable]
    public class Prefab
    {
        /// <summary>
        /// Имя префаба, которое унаследует игровой объект.
        /// </summary>
        public string PrefabName;
        /// <summary>
        /// Игровой объект который 
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// Состояние использования объекта
        /// </summary>
        public bool isUsed { get { return gameObject.activeSelf; } set { gameObject.SetActive(value); } }
        /// <summary>
        /// Ссылка на найденый компонент, реализующий интерфейс IElementObjectPool.
        /// Необходим для обработки деактивации игрового объекта  
        /// </summary>
        private IElementObjectPool IElement;

        private Prefab(GameObject gameObject, string name)
        {
            this.gameObject = gameObject;
            this.PrefabName = name;
            this.gameObject.name = name;
            this.isUsed = false;
            GameObject.DontDestroyOnLoad(this.gameObject);
            this.IElement = gameObject.GetComponent<IElementObjectPool>();
            IElement.OnDisableEvent += IElement_OnDisableEventHandler; ;
            gameObject.SetActive(isUsed);
        }

        /// <summary>
        /// Обрабатывает деактивацию объекта
        /// </summary>
        /// <param name="gameObject"></param>
        private void IElement_OnDisableEventHandler(GameObject gameObject)
        {
            if (gameObject.activeSelf == true)
                gameObject.SetActive(false);
            else
            {
                gameObject.transform.position = Vector3.zero;
                isUsed = false;
                gameObject.name = this.PrefabName;
            }
        }

        /// <summary>
        /// Копирует заданое количество префабов и добавляет в колецию.
        /// </summary>
        /// <returns></returns>
        /// 
        public void CloneByCount(System.Collections.Generic.List<Prefab> pool, byte count)
        {
            for (byte itemId = 0; itemId < count; itemId++)
                pool.Add(this.Clone());
        }

        /// <summary>
        /// Возвращает свою копию
        /// </summary>
        /// <returns></returns>
        public Prefab Clone()
        {
            return new Prefab(GameObject.Instantiate(this.gameObject), this.PrefabName);
        }
    }
}

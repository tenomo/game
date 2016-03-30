using UnityEngine;
using System.Collections;
namespace GameLogic
{
    public class GameBuilder : MonoBehaviour
    {
        // В подобных классах желательно юзать синглетон... 


        // Для примера префабы...
        [SerializeField]
        private GameObject prefab_sphere; // Ссылка на сферу в ассетс
        [SerializeField]
        private GameObject prefab_TEHandler; // Ссылка на "обработчик" в асетс. Что бы показать наглядно, это будут 
                                             // объекты без компонентов, которые соберуться внутри билдера...
                                             // Нюанс в том что это редко юзабельно - лишнего кода много.


        [SerializeField]
        private GameObject prefab_EventHadler; // Ссылка на префаб обработчика событий.вот тут все необходимое уже прикручено,пользуем.

        private EventHandler eventHandler; // Ссылка на комонент-класс

        // Use this for initialization
        void Start()
        {
            this.eventHandler = prefab_EventHadler.GetComponent<EventHandler>();
            eventHandler.ClickBuildGameObjects += EventHandler_ClickBuildGameObjects;
        }

        void CreateTrigerEnterHanlerOBJ()
        {
            // Создали объект на сцене
            prefab_TEHandler = Instantiate(prefab_TEHandler, Vector3.zero, Quaternion.identity) as GameObject;
            // Делаем из колайдера тригер...
            prefab_TEHandler.GetComponent<Collider>().isTrigger = true;       
            prefab_TEHandler.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);     // Это для колорита )
            prefab_TEHandler.AddComponent<TriggerEnterHandler>();

        }


        
        /// <summary>
        /// Создает 2 объекта шар 1 шар 2
        /// </summary>
        void BuildSpheres()
        {

            // iuse magic date :D

            Vector2 startPos = new Vector2(-5, 0);  
            GameObject tmp_go;
            for (int i = 0; i < 2; i++) 
            {
                tmp_go = Instantiate(prefab_sphere, startPos, Quaternion.identity) as GameObject; // создали шар на сцене
                tmp_go.name += " " + i.ToString();
                Rigidbody tmp_rigidbody = tmp_go.AddComponent<Rigidbody>();  // добавили компонент RigeBody -  обработка физики.

                // Debug.Log(tmp_rigidbody.name); кстати выведет в консоль лог

                tmp_rigidbody.useGravity = false; // гравитация не нужна
                tmp_rigidbody.isKinematic = true; // обработка столкновений не нужна
                
               ISphereBehaviour ISB = tmp_go.AddComponent<SphereBehaviour>(); // добавляем объекту компонент и сразу заносим в переменную

                if (i == 0)
                    ISB.side = Side.ringht;
                else
                    ISB.side = Side.left;

                ISB.speed = 1.3f ; //  ...

                startPos += new Vector2(10, 0); // следующий елемент будет на 10 точек правее 5,0

              
                eventHandler.ClickRun += ISB.Run;  
                
            }
        }

        private void EventHandler_ClickBuildGameObjects()
        {
            this.CreateTrigerEnterHanlerOBJ();
            this.BuildSpheres();
        }
    }


    // GameBuilder: - I need refactoring!   ... and interfaces :(  
    // :D
}
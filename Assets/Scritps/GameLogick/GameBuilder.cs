using UnityEngine;
using System.Collections;
namespace GameLogic
{
    public class GameBuilder : MonoBehaviour
    {
        // Юзать синглетон тут желательно... 


        // Для примера префабы...
        [SerializeField]
        private GameObject prefab_sphere; // Ссылка на сферу в асетс
        [SerializeField]
        private GameObject prefab_TEHandler; // Ссылка на "обработчик" в асетс. Что бы показать наглядно, это будут 
                                             // объекты без компонентов, которые соберуться внутри билдера...
                                             // Нюанс в том что это редко юзабельно - лишнего кода много.


        [SerializeField]
        private GameObject prefab_EventHadler; // Ссылка на префаб обработчика событий.вот тут все необходимое уже будет.

        private EventHandler eventHandler; // Ссылка на комонент-класс

        // Use this for initialization
        void Start()
        {
            this.eventHandler = prefab_EventHadler.GetComponent<EventHandler>();
            eventHandler.ClickBuildGameObjects += EventHandler_ClickBuildGameObjects;
        }

        void CreateTrigerEnterHanlerOBJ()
        {
            // Создали обїект на сцене
            prefab_TEHandler = Instantiate(prefab_TEHandler, Vector3.zero, Quaternion.identity) as GameObject;
            // Изменили значение коллайдера он тригер на тру,что бы работал ОнТригерЭнтер
            prefab_TEHandler.GetComponent<Collider>().isTrigger = true;
            // Это для колорита )
            prefab_TEHandler.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            prefab_TEHandler.AddComponent<TriggerEnterHandler>();

        }


        // magic date :D

        void BuildSpheres()
        {
            Vector2 startPos = new Vector2(-5, 0); // Позиция первого шара
            GameObject tmp_go;
            for (int i = 0; i < 2; i++) 
            {
                tmp_go = Instantiate(prefab_sphere, startPos, Quaternion.identity) as GameObject; // создали шар на сцене
                
                Rigidbody tmp_rigidbody = tmp_go.AddComponent<Rigidbody>();  // добавили компонент RigeBody -  обработка физики.
                Debug.Log(tmp_rigidbody.name);
                tmp_rigidbody.useGravity = false; // гравитация не нужна
                tmp_rigidbody.isKinematic = true; // столкновение не нужны

               ISphereBehaviour ISB = tmp_go.AddComponent<SphereBehaviour>(); // добавляем объекту компонент и сразу заносим в переменную
                if (i == 0)
                    ISB.side = Side.ringht;
                else ISB.side = Side.left;
                ISB.speed = 1.3f ; 
                startPos += new Vector2(10, 0); // следующий елемент будет на 10 точек правее 5,0

                // подписка на событие.
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
using UnityEngine;
using System.Collections;

namespace GameLogic
{ 
     
    public class TriggerEnterHandler : MonoBehaviour
    {
        /// <summary>
        /// Обрабатывает соприкосновение коллайдеров. 
        /// </summary>
        /// <param name="other"></param>
        public void OnTriggerEnter(Collider other)
        {

            // Тут все прозрачно но на всякий случай....
            // Вопервых метод почему то работает одновременно со всеми входящими объектами. Учитывая что мы в конце уничтожаем,
            // наш наблюдатель-обработчик, то явно код не повторяеться и рабтает одновременно для (двух) объектов. 
            // Не знаю почему так. изначально пробовал other заносить в список. Но остановился на варинте ниже,работает и славно,
            // пусть и не понятно

            // Создание примита (шар). Используеться дефолтная фабрика  GameObject
            GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere); //GameObject.Instantiate(C
            // Присваеваем ему позицию текущего объекта - наблюдателя.
            newGameObject.transform.position = this.transform.position;
            // Обнуление скейл
            newGameObject.transform.localScale = Vector3.zero;

            // Здесь храним сумму двух вдодящих объектов.
            Vector3 SumOthersOBJScale = Vector3.zero;       
            SumOthersOBJScale += other.transform.localScale*2;   //   += Работает криво, не разберался умножил на 2

            newGameObject.transform.localScale = SumOthersOBJScale;
             
             // Уничтожение входящих объектов.
             other.GetComponent<ISphereBehaviour>().DestroySphere();      

            // Уничтожение текущий обьект - наблюдатель.
            Destroy(this.gameObject);
         

        }
    }
}

using UnityEngine;
using System.Collections;

namespace GameLogic
{
    public class TriggerEnterHandler : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {


            // Тут все прозрачно но на всякий рапишу.
            // Вопервых метод почему то работает одновременно со всеми входящими объектами. Учитывая что мы в конце уничтожаем,
            // наш наблюдатель-обработчик, то явно код не повторяеться и рабтает одновременно для (двух) объектов. 
            // Не знаю почему так. изначально пробовал other заносить в список. работает - самое главное.


            // Далее создаем примитив 
            GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere); //GameObject.Instantiate(C
            // Присваеваем ему позицию текущего объекта - наблюдателя.
            newGameObject.transform.position = this.transform.position;
            // Обнуляем скейл - маштаб (размер)
            newGameObject.transform.localScale = Vector3.zero;

            // Здесь храним сумму двух вдодящих объектов.
            Vector3 SumOthersOBJScale = Vector3.zero;       

            SumOthersOBJScale += other.transform.localScale*2;      

            newGameObject.transform.localScale = SumOthersOBJScale;
             
             // Уничтожаем входящие объекты.
             other.GetComponent<ISphereBehaviour>().DestroySphere();  
    

            // Уничтожаем текущий обьект - наблюдателя
            Destroy(this.gameObject);
         

        }
    }
}

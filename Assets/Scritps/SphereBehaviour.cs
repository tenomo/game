using UnityEngine;
using System.Collections;
namespace GameLogic
{
    /// <summary>
    /// Список предоставляющий варинты направления.
    /// </summary>
    public enum Side { left = -1, ringht = 1 };

    public class SphereBehaviour : MonoBehaviour, ISphereBehaviour
    {

        [UnityEngine.SerializeField]  //  Атрибут для сериализации поля, что бы свойство отобразилось в редакторе.
        private Side _side;

        /// <summary>
        /// Направление движения.
        /// </summary>
        public Side side { get { return this._side; } set { this._side = value; } }

        [UnityEngine.SerializeField]  // Аналогично...  
        private float _speed;
        /// <summary>
        /// Скорость движния объекта.
        /// </summary>
        public float speed { get { return _speed; } set { _speed = value; } }


        // Кешируем "трансформ", иначе в апдейте будет работать 
        // конструкция gameObject.transfrom - тоесть вот так : GetComponent<Transfrom>();, что бьет по ресурсам        
        private Transform cachTransfrom;

        // Use this for initialization
        void Start()
        {
            // Всего 1 раз вызываем GetComponent<Transfrom>();
            this.cachTransfrom = this.gameObject.GetComponent<Transform>();

            if (speed <= 0)
                speed = 0.5f; // default value.

            // debug test
            // this.Move();
        }

        // Update is called once per frame
        void Update()
        {
            // Move можно вызывать тут, но если хотим активировать дввижение объекта
            //по событию красиво,не используя булевые переменные, то.... (ниже)

        }

        // https://habrahabr.ru/post/216185/
        // Это эмулятор потока в юнити, обычные потоки не потдерживаються движком
        // и поток будет вне игры как отдельная программа.
        // Но есть корунтайны - работают паралельно апдейт.
        private IEnumerator MoveCoroutine()
        {
            while (true) // Двигаться пока...
            {

                if (this.side == Side.left)
                {
                    this.cachTransfrom.Translate(Vector2.left * this.speed * Time.deltaTime);

                    if (this.cachTransfrom.position.x <= 0)  // Если в лево, пока х меньше 0
                        break;
                }
                else if (this.side == Side.ringht)
                {
                    this.cachTransfrom.Translate(Vector2.right * this.speed * Time.deltaTime);

                    if (this.cachTransfrom.position.x >= 0) // Если в право, пока х не больше 0
                        break;
                }

                // Тут приделать защиту от дурака чтобы эта конcтрукция не зацbклилась )

                yield return null; // Выходит с Coroutine
            }
        }

        public void Run()
        {
            this.StartCoroutine(this.MoveCoroutine());
        }

        // Это на случай если захотим прикрутить анимацию или скрыть за абстракцией возврат в пул вместо уничтожения.
        // Кстати дестрой тоже ест ресурсы помоему.... 
        public void DestroySphere()
        {
            Destroy(this.gameObject);
        }
    }

    public interface ISphereBehaviour
    {
        void Run();
        float speed { get; set; }
        Side side { get; set; }
        void DestroySphere();
    }

}
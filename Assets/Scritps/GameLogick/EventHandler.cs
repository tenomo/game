using UnityEngine;
using System.Collections;


// Не знаю на сколько актуален данный объект. В редакторе,выбрав кнопку можно назначить выполняемый метод.
// Но у нас 2 шара потому.
public class EventHandler : MonoBehaviour {

   // Тут лучше юзать синглетон 

    public delegate void ButtonClickHandler();
    public event ButtonClickHandler ClickRun;
    public event ButtonClickHandler ClickBuildGameObjects;

     // Эти методы назначаються кнопке в редакторе и вызыаються при нажатии. 
  
    #region perform events 
    public void Alert_ButtonClick_Run()
    {
        if (this.ClickRun != null)
            this.ClickRun();
    }

    public void Alert_ButtonClick_BuildGameObjects()
    {
        if (this.ClickBuildGameObjects != null)
            this.ClickBuildGameObjects();
    }
    #endregion
}

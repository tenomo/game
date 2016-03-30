namespace  ObjectPool
{
    public delegate void OnDisableEventHandler(UnityEngine.GameObject gameObject);
    /// <summary>
    /// Интерфейс объекта содержащегося в объектном пуле.
    /// </summary>
    public interface IElementObjectPool
    {
        event OnDisableEventHandler OnDisableEvent; 
    }
}

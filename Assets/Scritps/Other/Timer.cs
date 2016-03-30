using UnityEngine;
using System.Collections;

  delegate void ElapsedHanler();
class Timer : MonoBehaviour , ITimer
{
   public float interval { get; set; }
    private float _time;
  
    public event ElapsedHanler Elapsed;

    public float time
    {
        get { return _time; }
        set
        {
            _time = value;
            if (_time >= interval)
                _time = 0;
            else if (_time < 0)
                _time = 0;
        }
    }

    public void startTimer()
    {
        StartCoroutine(StartTimerCourutine());
    }

    private void Start()
    {
        this.time = 0;
    }

    private void Update ()
    {
        time += Time.deltaTime;        
    }

   private IEnumerator StartTimerCourutine()
    {
        yield return new WaitForSeconds(interval);
        if (Elapsed != null)
            Elapsed();
    }

    public static Timer AddTimer(GameObject obj)
    {
      return  obj.AddComponent<Timer>();
    }      
}
 
     
 
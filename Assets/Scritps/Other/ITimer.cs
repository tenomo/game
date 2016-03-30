internal interface ITimer
{
    float interval { get; set; }
    event ElapsedHanler Elapsed;
    float time { get; set; }
    void startTimer();
}
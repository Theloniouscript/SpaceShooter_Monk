using System.ComponentModel;

public class Timer 
{
    private float m_CurrentTime;
    public bool IsFinished => m_CurrentTime <= 0;

    public Timer(float startTime)
    {
        Start(startTime);
    }


    // Start is called before the first frame update
    public void Start(float startTime)
    {
        m_CurrentTime= startTime;
    }

    // Update is called once per frame
    public void RemoveTime(float deltaTime)
    {
        if(m_CurrentTime <= 0) return;
        m_CurrentTime -= deltaTime;
    }
}

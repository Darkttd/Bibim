namespace Bibim
{
    float EvalTimeflow::GetTime() const
    {
        return time;
    }

    float EvalTimeflow::GetDuration() const
    {
        return duration;
    }

    float EvalTimeflow::GetVelocity() const
    {
        return velocity;
    }

    void EvalTimeflow::SetVelocity(float value)
    {
        velocity = value;
    }

    bool EvalTimeflow::GetLooped() const
    {
        return looped;
    }

    void EvalTimeflow::SetLooped(bool value)
    {
        looped = value;
    }
}
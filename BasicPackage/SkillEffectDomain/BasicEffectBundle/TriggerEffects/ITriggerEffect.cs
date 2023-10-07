namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    /// <summary>
    /// trigger effect active while trig.
    /// use different trigger and set effect.
    /// </summary>
    public interface ITriggerEffect:IBuffEffect
    {
        void Active();
    }
}
namespace InfinityWorldChess.SkillDomain
{
    public interface ICoreSkill : IActiveSkill
    {
        /// <summary>
        ///     make sure the code and the layer is matched
        /// </summary>
        byte FullCode { get; }

        byte MaxLayer { get;  }
    }
}
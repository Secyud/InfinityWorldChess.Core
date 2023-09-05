#region

using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		public NatureProperty Nature { get; } = new();

		public class NatureProperty : IArchivable
		{
			public readonly float[] Properties = new float[SharedConsts.NatureCount];
// 认知
			public float Recognize
			{
				get => Properties[0];
				set => Properties[0] = value;
			}
// 稳定 
			public float Stability
			{
				get => Properties[1];
				set => Properties[1] = value;
			}
			// 能力 
			public float Confident
			{
				get => Properties[2];
				set => Properties[2] = value;
			}
			// 效益
			public float Efficient
			{
				get => Properties[3];
				set => Properties[3] = value;
			}
// 合群
			public float Gregarious
			{
				get => Properties[4];
				set => Properties[4] = value;
			}
			// 利他
			public float Altruistic
			{
				get => Properties[5];
				set => Properties[5] = value;
			}
			// 理性 
			public float Rationality
			{
				get => Properties[6];
				set => Properties[6] = value;
			}
			// 远见
			public float Foresighted
			{
				get => Properties[7];
				set => Properties[7] = value;
			}
			// 渊博
			public float Intelligent
			{
				get => Properties[8];
				set => Properties[8] = value;
			}

			public void Save(IArchiveWriter writer)
			{
				foreach (float property in Properties)
					writer.Write(property);
			}

			public void Load(IArchiveReader reader)
			{
				for (int i = 0; i < Properties.Length; i++)
					Properties[i] = reader.ReadSingle();
			}
		}
	}
}
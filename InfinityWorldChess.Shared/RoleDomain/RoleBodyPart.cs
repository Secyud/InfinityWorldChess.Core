#region

using Secyud.Ugf.Archiving;
using System;
using System.IO;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class RoleBodyPart : IArchivable
	{
		private float _realValue;

		public int MaxValue { get; set; }

		public float RealValue
		{
			get => _realValue;
			set => _realValue = Math.Min(MaxValue, value);
		}


		public override string ToString()
		{
			return $"{RealValue}/{MaxValue}";
		}

		public void Save(BinaryWriter writer)
		{
			writer.Write(MaxValue);
			writer.Write(RealValue);
		}

		public void Load(BinaryReader reader)
		{
			MaxValue = reader.ReadInt32();
			RealValue = reader.ReadInt32();
		}
	}
}
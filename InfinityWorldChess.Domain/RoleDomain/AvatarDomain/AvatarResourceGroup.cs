#region

using Secyud.Ugf.Collections;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class AvatarResourceGroup
	{
		public readonly RegistrableDictionary<int, AvatarSpriteContainer> BackItem = new();
		public readonly RegistrableDictionary<int, AvatarSpriteContainer> BackHair = new();
		public readonly RegistrableDictionary<int, AvatarSpriteContainer> Body = new();
		public readonly RegistrableDictionary<int, AvatarSpriteContainer2> Head = new();
		public readonly RegistrableDictionary<int, AvatarSpriteContainer> HeadFeature = new();
		public readonly RegistrableDictionary<int, AvatarSpriteContainer> Nose = new();
		public readonly RegistrableDictionary<int, AvatarSpriteContainer> Mouth = new();
		public readonly RegistrableDictionary<int, AvatarSpriteContainer2> Eye = new();
		public readonly RegistrableDictionary<int, AvatarSpriteContainer2> Brow = new();
		public readonly RegistrableDictionary<int, AvatarSpriteContainer> FrontHair = new();
	}
}
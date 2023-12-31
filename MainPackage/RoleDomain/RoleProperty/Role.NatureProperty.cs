#region

using System;
using System.Runtime.InteropServices;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        [field: S] public NatureProperty Nature { get; } = new();

        [Guid("D8CC09D0-C472-64B5-7A32-FBFB0294795C")]
        public class NatureProperty : IArchivable
        {
            // 认知
            public float Recognize { get; set; }

            // 稳定 
            public float Stability { get; set; }

            // 能力 
            public float Confident { get; set; }

            // 效益
            public float Efficient { get; set; }

            // 合群
            public float Gregarious { get; set; }

            // 利他
            public float Altruistic { get; set; }

            // 理性 
            public float Rationality { get; set; }

            // 远见
            public float Foresighted { get; set; }

            // 渊博
            public float Intelligent { get; set; }

            /// <summary>
            /// 好像没啥用，简化保存？或者提供给后续使用。
            /// </summary>
            /// <param name="i"></param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            private float this[int i]
            {
                get
                {
                    return i switch
                    {
                        0 => Recognize, 1  => Stability, 2   => Confident, 3   => Efficient, 4 => Gregarious,
                        5 => Altruistic, 6 => Rationality, 7 => Foresighted, 8 => Intelligent,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                set
                {
                    switch (i)
                    {
                        case 0:
                            Recognize = value;
                            break;
                        case 1:
                            Stability = value;
                            break;
                        case 2:
                            Confident = value;
                            break;
                        case 3:
                            Efficient = value;
                            break;
                        case 4:
                            Gregarious = value;
                            break;
                        case 5:
                            Altruistic = value;
                            break;
                        case 6:
                            Rationality = value;
                            break;
                        case 7:
                            Foresighted = value;
                            break;
                        case 8:
                            Intelligent = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            public void Save(IArchiveWriter writer)
            {
                for (int i = 0; i < MainPackageConsts.NatureCount; i++)
                {
                    writer.Write(this[i]);
                }
            }

            public void Load(IArchiveReader reader)
            {
                for (int i = 0; i < MainPackageConsts.NatureCount; i++)
                {
                    this[i] = reader.ReadSingle();
                }
            }
        }
    }
}
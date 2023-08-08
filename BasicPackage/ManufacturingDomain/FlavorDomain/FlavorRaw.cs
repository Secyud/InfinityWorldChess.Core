using System;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.FlavorDomain
{
    public abstract class FlavorRaw : Manufacturable, IHasFlavor
    {
        [field: S(ID = 256)] public float[] FlavorLevel { get; } = new float[BasicConsts.FlavorCount];

        public abstract Color Color { get; }

        public virtual void Manufacturing(Manufacture manufacture, FlavorProcessData processData)
        {
            for (int i = 0; i < BasicConsts.FlavorCount; i++)
            for (int j = 0; j < BasicConsts.FlavorCount; j++)
            {
                float di = processData.FlavorLevel[i];
                float dj = processData.FlavorLevel[j];
                float oi = FlavorLevel[i];
                float oj = FlavorLevel[j];

                switch (i - j)
                {
                    case 0:
                        processData.DragProperty[i, j] += oi + oj + di + dj;
                        break;
                    case 1:
                    case -4:
                        processData.DragProperty[i, j] += oi + 0.5f * oj + 2 * di + dj;
                        break;
                    case 2:
                    case -3:
                        processData.DragProperty[i, j] += -oi + oj + di + dj;
                        break;
                    case 3:
                    case -2:
                        processData.DragProperty[i, j] += 0.5f * oi + oj + di + 2 * dj;
                        break;
                    case 4:
                    case -1:
                        processData.DragProperty[i, j] += oi - oj + di + dj;
                        break;
                }
            }
        }


        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddFlavorInfo(this);
        }
        
        protected static readonly int[] S = { 3, 4, 0, 1, 2, 3, 4, 0, 1 };
    }
}
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillPlayDomain
{
    public class TargetPlay:SkillPlay
    {
        [SerializeField] private GameObject TargetObject;
        [SerializeField] private bool Role;
        private GameObject _instance;


        public override void Play(HexUnit unit, HexCell targetCell)
        {
            base.Play(unit, targetCell);
             
            if(_instance)
                Destroy(_instance);
            Transform parent;
            if (Role && targetCell.Unit)
            {
                parent = targetCell.Unit.transform;
            }
            else
            {
                parent = targetCell.transform;
            }

            _instance = Instantiate(TargetObject, parent);
        }

        protected override void EndPlay()
        {
            base.EndPlay();
            Destroy(_instance);
        }
    }
}
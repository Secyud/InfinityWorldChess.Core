using Secyud.Ugf.UgfHexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillPlayDomain
{
    public class TargetPlay:SkillPlay
    {
        [SerializeField] private GameObject TargetObject;
        [SerializeField] private bool Role;
        private GameObject _instance;


        public override void Play(UgfUnit unit, UgfCell targetCell)
        {
            base.Play(unit, targetCell);
             
            if(_instance)
                Destroy(_instance);
            Transform parent;
            if (Role && targetCell.Cell.Unit)
            {
                parent = targetCell.Cell.Unit.transform;
            }
            else
            {
                parent = targetCell.Cell.transform;
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
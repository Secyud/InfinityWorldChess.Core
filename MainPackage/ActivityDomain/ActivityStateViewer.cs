using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityStateViewer : MonoBehaviour
    {
        [SerializeField] private SImage Graph;
        [SerializeField] private Sprite Finished;
        [SerializeField] private Sprite Received;
        [SerializeField] private Sprite NotReceived;
        [SerializeField] private Sprite Failed;

        public void SetState(ActivityState state)
        {
            Graph.Sprite = state switch
            {
                ActivityState.NotReceived => NotReceived,
                ActivityState.Failed      => Failed,
                ActivityState.Received    => Received,
                ActivityState.Success    => Finished,
                _                         => null
            };
        }
    }
}
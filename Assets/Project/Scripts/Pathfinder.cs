using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Project.Scripts.Project.Scripts;
using UnityEngine;

namespace Project.Scripts
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField]
        private Color startColor = Color.green;

        [SerializeField]
        private Color goalColor = Color.red;

        [SerializeField]
        private Color frontierColor = Color.magenta;

        [SerializeField]
        private Color exploredColor = Color.gray;

        [SerializeField]
        private Color pathColor = Color.cyan;

        private Node _startNode;
        private Node _goalNode;

        private Graph _graph;
        private GraphView _graphView;

        private Queue<Node> _frontierNodes;
        private List<Node> _exploredNodes;
        private List<Node> _pathNodes;

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public void Initialize([NotNull] Graph graph, [NotNull] GraphView graphView, [NotNull] Node start,
            [NotNull] Node goal)
        {
            if (graph == null || graphView == null || start == null || goal == null)
            {
                Debug.LogWarning("Invalid data passed to initialize.");
                return;
            }

            if (start.Type == NodeType.Blocked || goal.Type == NodeType.Blocked)
            {
                Debug.LogWarning("Invalid start or end nodes selected.");
                return;
            }

            _graph = graph;
            _graphView = graphView;
            _startNode = start;
            _goalNode = goal;

            var startNodeView = graphView.GetView(start);
            if (startNodeView != null) startNodeView.ColorNode(startColor);

            var goalNodeView = graphView.GetView(goal);
            if (goalNodeView != null) goalNodeView.ColorNode(goalColor);

            _frontierNodes = new Queue<Node>();
            _frontierNodes.Enqueue(start);

            _exploredNodes = new List<Node>();
            _pathNodes = new List<Node>();

            for (var x = 0; x < graph.Width; ++x)
            {
                for (var y = 0; y < graph.Height; ++y)
                {
                    var node = graph.Nodes[x, y];
                    node?.Reset();
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace TPKSLabs.Helpers
{

    public class DextraHelper
    {

        private readonly int[,] _topologyMatrix;
        private int[,] _nodesShortestDistance { get; set; }

        #region .ctor

        public DextraHelper(int[,] topologyMatrix)
        {
            _topologyMatrix = topologyMatrix;

            _nodesShortestDistance = new int[_topologyMatrix.GetLength(0), _topologyMatrix.GetLength(1)];
        }

        #endregion

        public int[,] CalculateShortestDistances()
        {
            //cycle for every node
            for (var i = 0; i < _topologyMatrix.GetLength(0); i++)
            {
                CalculateClosestPathes(i);
            }

            return _nodesShortestDistance;

        }

        #region Private Methods

        private void CalculateClosestPathes(int nodeId)
        {
            int[] pathArray = GetArrayOfMaxIntegers(_topologyMatrix.GetLength(1));

            SortedSet<int> calculatedNodes = new SortedSet<int>();
            int currentNodeId = nodeId;

            pathArray[nodeId] = 0;



            while (calculatedNodes.Count != pathArray.Length)
            {
                //get visited node, which wasnt calculated
                currentNodeId = GetNodeWithMinMark(calculatedNodes, pathArray);

                for (int i = 0; i < _topologyMatrix.GetLength(1); i++)
                {
                    if (_topologyMatrix[currentNodeId, i]==0) continue;

                    int pathLength = pathArray[currentNodeId] + _topologyMatrix[currentNodeId, i];

                    pathArray[i] = (pathLength < pathArray[i]) ? pathLength : pathArray[i];

                }
                calculatedNodes.Add(currentNodeId);
            }

            for (var i = 0; i<pathArray.Length; i++)
            {
                _nodesShortestDistance[nodeId, i] = pathArray[i];
            }


        }

        private int[] GetArrayOfMaxIntegers(int length)
        {
            return Enumerable.Repeat(int.MaxValue, length).ToArray();
        }

        private int GetNodeWithMinMark(SortedSet<int> visitedNodes, int[] pathArray)
        {
            int minIndex = -1;
            int minValue = int.MaxValue;

            for (int i = 0; i < pathArray.Length; i++)
            {
                if (visitedNodes.Contains(i)) continue;

                if (pathArray[i] < minValue)
                {
                    minIndex = i;
                    minValue = pathArray[i];
                }
            }
            return minIndex;
        }
        #endregion
    }
}
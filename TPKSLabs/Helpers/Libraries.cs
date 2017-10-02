using System.Collections.Generic;

namespace TPKSLabs.Helpers
{
    public enum ClusterType
    {
        DoubleStar
    }

    public class Libraries
    {
        public static Dictionary<ClusterType, byte[,]> ClusterMatrices = new Dictionary<ClusterType, byte[,]>()
        {
            {ClusterType.DoubleStar, new byte[,]
            {
                {0,0,0,1,1,0,0,0 }, //0
                {0,0,1,0,1,0,0,0 }, //1
                {0,1,0,1,0,0,0,0 }, //2
                {1,0,1,0,0,0,1,1 }, //3
                {1,1,0,0,0,1,0,1 }, //4
                {0,0,0,0,1,0,1,0 }, //5
                {0,0,0,1,0,1,0,0 }, //6
                {0,0,0,1,1,0,0,0 }  //7
            } }
        };

        public static Dictionary<ClusterType, List<ConnectionItem>> TopologyRule = new Dictionary<ClusterType, List<ConnectionItem>>()
        {
            {ClusterType.DoubleStar, new List<ConnectionItem>()
            {
                new ConnectionItem(1, 2),
                new ConnectionItem(2, 1),
                new ConnectionItem(5, 5),
                new ConnectionItem(6, 6),
                new ConnectionItem(7, 0)
            } }
        };
    }
}
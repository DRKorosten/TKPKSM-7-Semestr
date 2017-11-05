using System.Collections.Generic;

namespace TPKSLabs.Helpers
{
    public enum ClusterType
    {
        Cluster_Lab1,
        Cluster_Lab2
    }

    public class Libraries
    {
        public static Dictionary<ClusterType, byte[,]> ClusterMatrices = new Dictionary<ClusterType, byte[,]>()
        {
            {
                ClusterType.Cluster_Lab1, new byte[,]{
                   //0,1,2,3,4,5,6,7,8
                    {0,0,1,1,0,0,0,0,0 }, //0
                    {0,0,0,0,1,1,0,1,0 }, //1
                    {1,0,0,0,0,1,0,0,1 }, //2
                    {1,0,0,0,1,0,1,0,0 }, //3
                    {0,1,0,1,0,1,0,0,0 }, //4
                    {0,0,1,0,1,0,0,0,0 }, //5
                    {0,0,0,1,0,0,0,1,0 }, //6
                    {0,1,0,0,0,0,1,0,1 }, //7
                    {0,0,1,0,0,0,0,1,0 }  //8
                }
            },
            {
                ClusterType.Cluster_Lab2, new byte[,]{
                   //0,1,2,3,4,5
                    {0,1,0,1,0,1 }, //0
                    {1,0,1,0,1,0 }, //1
                    {0,1,0,1,0,1 }, //2
                    {1,0,1,0,1,0 }, //3
                    {0,1,0,1,0,1 }, //4
                    {1,0,1,0,1,0 }, //5
                }
            }
        };

        public static Dictionary<ClusterType, List<ConnectionItem>> Lab1_Rule = new Dictionary<ClusterType, List<ConnectionItem>>()
        {
            {ClusterType.Cluster_Lab2, new List<ConnectionItem>()
            {
                new ConnectionItem(2, 0),
                new ConnectionItem(5, 3),
                new ConnectionItem(8, 6),
                new ConnectionItem(4, 4),
                new ConnectionItem(0, 2),
                new ConnectionItem(3, 5),
                new ConnectionItem(6, 8),
            } }
        };
        
        public static Dictionary<ClusterType, List<ConnectionItem>> Lab2_Row_Inner_Rule = new Dictionary<ClusterType, List<ConnectionItem>>()
        {
            {ClusterType.Cluster_Lab2, new List<ConnectionItem>()
            {
                new ConnectionItem(0, 0),
                new ConnectionItem(3, 3),
                new ConnectionItem(1, 5),
                new ConnectionItem(2, 4)
            } }
        };
        public static Dictionary<ClusterType, List<ConnectionItem>> Lab2_Row_Outer_Rule = new Dictionary<ClusterType, List<ConnectionItem>>()
        {
            {ClusterType.Cluster_Lab2, new List<ConnectionItem>()
            {
                new ConnectionItem(1, 5),
                new ConnectionItem(2, 4)
            } }
        }; public static Dictionary<ClusterType, List<ConnectionItem>> Lab2_Col_Inner_Rule = new Dictionary<ClusterType, List<ConnectionItem>>()
        {
            {ClusterType.Cluster_Lab2, new List<ConnectionItem>()
            {
                new ConnectionItem(3, 0)
            } }
        }; public static Dictionary<ClusterType, List<ConnectionItem>> Lab2_Col_Outer_Rule = new Dictionary<ClusterType, List<ConnectionItem>>()
        {
            {ClusterType.Cluster_Lab2, new List<ConnectionItem>()
            {
                new ConnectionItem(3, 0)
            } }
        };

    }
}
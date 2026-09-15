using UnityEngine;
using System.Collections.Generic;

public static class WallKickData
{
    // 5 thử nghiệm dịch chuyển (Kick Tests) cho các hướng xoay (0->1, 1->0, 1->2, 2->1, 2->3, 3->2, 3->0, 0->3)
    public static readonly Dictionary<string, Vector2Int[]> JLSTZ_WallKicks = new Dictionary<string, Vector2Int[]>()
    {
        { "0->1", new Vector2Int[] { new(0,0), new(-1,0), new(-1,1), new(0,-2), new(-1,-2) } },
        { "1->0", new Vector2Int[] { new(0,0), new(1,0),  new(1,-1), new(0,2),  new(1,2) } },
        { "1->2", new Vector2Int[] { new(0,0), new(1,0),  new(1,-1), new(0,2),  new(1,2) } },
        { "2->1", new Vector2Int[] { new(0,0), new(-1,0), new(-1,1), new(0,-2), new(-1,-2) } },
        { "2->3", new Vector2Int[] { new(0,0), new(1,0),  new(1,1),  new(0,-2), new(1,-2) } },
        { "3->2", new Vector2Int[] { new(0,0), new(-1,0), new(-1,-1),new(0,2),  new(-1,2) } },
        { "3->0", new Vector2Int[] { new(0,0), new(-1,0), new(-1,-1),new(0,2),  new(-1,2) } },
        { "0->3", new Vector2Int[] { new(0,0), new(1,0),  new(1,1),  new(0,-2), new(1,-2) } },
    };

    public static readonly Dictionary<string, Vector2Int[]> I_WallKicks = new Dictionary<string, Vector2Int[]>()
    {
        { "0->1", new Vector2Int[] { new(0,0), new(-2,0), new(1,0),  new(-2,-1),new(1,2) } },
        { "1->0", new Vector2Int[] { new(0,0), new(2,0),  new(-1,0), new(2,1),  new(-1,-2) } },
        { "1->2", new Vector2Int[] { new(0,0), new(-1,0), new(2,0),  new(-1,2), new(2,-1) } },
        { "2->1", new Vector2Int[] { new(0,0), new(1,0),  new(-2,0), new(1,-2), new(-2,1) } },
        { "2->3", new Vector2Int[] { new(0,0), new(2,0),  new(-1,0), new(2,1),  new(-1,-2) } },
        { "3->2", new Vector2Int[] { new(0,0), new(-2,0), new(1,0),  new(-2,-1),new(1,2) } },
        { "3->0", new Vector2Int[] { new(0,0), new(1,0),  new(-2,0), new(1,-2), new(-2,1) } },
        { "0->3", new Vector2Int[] { new(0,0), new(-1,0), new(2,0),  new(-1,2), new(2,-1) } },
    };
}
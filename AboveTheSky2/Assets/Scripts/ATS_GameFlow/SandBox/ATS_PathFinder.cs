
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    [System.Serializable]
    public class ATS_Vector2Int : UCL.Core.JsonLib.UnityJsonSerializable
    {
        public int m_X;
        public int m_Y;
        public ATS_Vector2Int() { }
        public ATS_Vector2Int(int iX ,int iY)
        {
            m_X = iX; m_Y = iY;
        }
        public ATS_Vector2Int(Vector2Int iPos)
        {
            m_X = iPos.x; m_Y = iPos.y;
        }
        public Vector2Int ToVector2Int => new Vector2Int(m_X, m_Y);
        public void Set(Vector2Int iPos)
        {
            m_X = iPos.x;
            m_Y = iPos.y;
        }
    }
    public class ATS_Path : UCL.Core.JsonLib.UnityJsonSerializable
    {
        public List<ATS_Vector2Int> m_Path = new List<ATS_Vector2Int>();
    }
    public class PathNode
    {
        public Vector2Int m_Pos;
        public PathNode m_PrevNode;
        public int m_Distance = 0;
        public PathNode() { }
        public PathNode(Vector2Int pos, PathNode prevNode, int distance)
        {
            m_Pos = pos;
            m_PrevNode = prevNode;
            m_Distance = distance;
        }
    }
    public enum PathState : int
    {
        None = 0,
        Up = 1,
        Down  = 1 << 1,//1 << 1 == 0010
        Right = 1 << 2,//1 << 2 == 0100
        Left  = 1 << 3,//1 << 3 == 1000
        /// <summary>
        /// 標記為不可通行(無法進入此地塊)
        /// </summary>
        Obstacle = 1 << 4,//1 << 4 == 10000

        /// <summary>
        /// 標記為建築物占用(建築物不能有重疊)
        /// </summary>
        Occupied = 1 << 8,
        /// <summary>
        /// 可建造的地塊
        /// </summary>
        CanBuild = 1 << 9,
    }
    public class TilePathState : UCL.Core.JsonLib.UnityJsonSerializable, UCLI_FieldOnGUI
    {
        public int m_PathState = 0;

        public TilePathState()
        {

        }
        public TilePathState(int iPathState)
        {
            m_PathState = iPathState;
        }


        /// <summary>
        /// 判斷是否包含指定的PathState
        /// </summary>
        /// <param name="iPathState"></param>
        /// <returns></returns>
        public bool GetPathState(PathState iPathState) => (m_PathState & (int)iPathState) != 0;
        public void SetPathState(PathState iPathState, bool iFlag)
        {
            if (iFlag)
            {
                m_PathState |= (int)iPathState;
            }
            else//RemovePathState
            {
                //https://stackoverflow.com/questions/4778166/how-to-remove-an-item-for-a-ord-enum
                m_PathState &= ~(int)iPathState;
            }
        }


        /// <summary>
        /// return new data if the data of field altered
        /// </summary>
        /// <param name="iFieldName"></param>
        /// <param name="iDataDic"></param>
        /// <returns></returns>
        virtual public object OnGUI(string iFieldName, UCL_ObjectDictionary iDataDic)
        {
            UCL_GUILayout.DrawObjExSetting aDrawObjExSetting = new UCL_GUILayout.DrawObjExSetting();
            aDrawObjExSetting.OnShowField = () =>
            {
                foreach(PathState aPathState in System.Enum.GetValues(typeof(PathState)))
                {
                    if(aPathState is PathState.None)
                    {
                        continue;
                    }
                    GUILayout.BeginHorizontal();
                    SetPathState(aPathState, UCL_GUILayout.CheckBox(GetPathState(aPathState)));
                    GUILayout.Label($"{aPathState}", UCL_GUIStyle.LabelStyle);
                    
                    GUILayout.EndHorizontal();
                }
            };
            UCL_GUILayout.DrawField(this, iDataDic, iFieldName, iDrawObjExSetting: aDrawObjExSetting);

            return this;
        }
    }
    /// <summary>
    /// Find path base on region
    /// </summary>
    public class ATS_PathFinder : SandBoxBase
    {
        public static Dictionary<PathState, Vector2Int> PathStateDic
        {
            get
            {
                if(s_PathStateDic == null)
                {
                    s_PathStateDic = new Dictionary<PathState, Vector2Int>();
                    s_PathStateDic[PathState.Up] = Vector2Int.up;
                    s_PathStateDic[PathState.Down] = Vector2Int.down;
                    s_PathStateDic[PathState.Right] = Vector2Int.right;
                    s_PathStateDic[PathState.Left] = Vector2Int.left;
                }
                return s_PathStateDic;
            }
        }
        public static Dictionary<PathState, Vector2Int> s_PathStateDic = null; 

        public Cell[,] Cells => Region.Cells;
        public int Width { get; private set; }
        public int Height { get; private set; }
        /// <summary>
        /// 是否需要刷新路徑
        /// </summary>
        public bool RequireRefreshAllPathState { get; set; } = false;


        public override void Init(ATS_SandBox iSandBox, ISandBox iParent)
        {
            base.Init(iSandBox, iParent);
            var aRegion = Region;
            Width = aRegion.Width;
            Height = aRegion.Height;

            RefreshAllPathState();
        }
        public override void GameUpdate()
        {
            if (RequireRefreshAllPathState)
            {
                RefreshAllPathState();
            }
            base.GameUpdate();
        }
        public void RefreshAllPathState()
        {
            //var aCells = Cells;
            for (int y = 0; y < Height; y++) 
            {
                for (int x = 0; x < Width; x++)
                {
                    RefreshPathState(x, y);
                    //var aCell = aCells[x, y];
                }
            }
            RequireRefreshAllPathState = false;
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="targetX"></param>
        /// <param name="targetY"></param>
        /// <returns></returns>
        public ATS_Path FindPath(float x, float y, float targetX, float targetY)
        {
            return new ATS_Path();
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="targetX"></param>
        /// <param name="targetY"></param>
        /// <returns></returns>
        public ATS_Path SearchPath(float x, float y, System.Func<Cell, PathNode, bool> iCheckFunc, int iMaxDistance = 999)
        {
            const int MaxSearchTimes = 9999;
            HashSet<Vector2Int> aVisited = new HashSet<Vector2Int>();
            Queue<PathNode> aNodes = new Queue<PathNode>();

            var aStartPos = new Vector2Int(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
            PathNode aStart = new PathNode(aStartPos, null, 0);
            PathNode aTargetNode = null;
            aVisited.Add(aStartPos);
            aNodes.Enqueue(aStart);
            var aCells = Cells;
            int aSearchTimes = 0;
            while (aNodes.Count > 0 && aSearchTimes++ < MaxSearchTimes)
            {
                var aCurNode = aNodes.Dequeue();
                var aPos = aCurNode.m_Pos;
                var aCell = aCells[aPos.x, aPos.y];
                //Debug.LogError($"SearchPath aPos:{aPos}");
                bool aFindTarget = iCheckFunc(aCell, aCurNode);
                if (aFindTarget)
                {
                    aTargetNode = aCurNode;
                    //Debug.LogError($"SearchPath aFindTarget aPos:{aPos}");
                    break;
                }
                if(aCurNode.m_Distance < iMaxDistance)//仍在搜尋範圍內 繼續找下一個節點
                {
                    int aCurPathState = GetPathState(aPos.x, aPos.y);
                    foreach (var aPathState in PathStateDic.Keys)
                    {
                        if ((aCurPathState & (int)aPathState) != 0)//可以往此方向移動
                        {
                            var aDir = PathStateDic[aPathState];
                            var aNextPos = aPos + aDir;
                            if (!aVisited.Contains(aNextPos))//確保還沒走過
                            {
                                aVisited.Add(aNextPos);
                                aNodes.Enqueue(new PathNode(aNextPos, aCurNode, aCurNode.m_Distance + 1));


                            }
                        }
                    }
                }
            }
            //if(aTargetNode == null)//找不到目標
            //{
            //    return null;
            //}
            var aPath = new ATS_Path();
            var aNode = aTargetNode;
            if(aNode != null)
            {
                while (aNode != null)
                {
                    aPath.m_Path.Add(new ATS_Vector2Int(aNode.m_Pos));
                    aNode = aNode.m_PrevNode;
                }
                aPath.m_Path.Reverse();
            }

            return aPath;
        }

        public List<PathState> GetPaths(int x, int y)
        {
            List<PathState> aPaths = new List<PathState>();
            int aCurPathState = GetPathState(x, y);
            foreach (var aPathState in PathStateDic.Keys)
            {
                if ((aCurPathState & (int)aPathState) != 0)//可以往此方向移動
                {
                    aPaths.Add(aPathState);
                }
            }
            return aPaths;
        }
        public int GetSelfPathState(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return 0;
            }
            Cell aCell = Cells[x, y];
            return aCell.SelfPathState;
        }
        public int GetPathState(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return 0;
            }
            Cell aCell = Cells[x, y];
            return aCell.m_PathState;
        }
        /// <summary>
        /// 回傳相反方向
        /// </summary>
        /// <param name="iDir"></param>
        /// <returns></returns>
        public static PathState GetInversePathState(PathState iDir)
        {
            switch (iDir)
            {
                case PathState.Up: return PathState.Down;
                case PathState.Down: return PathState.Up;
                case PathState.Left: return PathState.Right;
                case PathState.Right: return PathState.Left;
            }
            return PathState.None;
        }
        public bool CheckCanEnter(int x, int y, PathState iDir)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return false;
            }

            int aPathState = GetSelfPathState(x, y);
            if((aPathState & (int)PathState.Obstacle) != 0)//障礙物
            {
                return false;
            }
            var aInversePathState = GetInversePathState(iDir);
            return ((aPathState & (int)aInversePathState) != 0);
        }
        public void RefreshPathState(int x, int y)
        {
            if(x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return;
            }
            Cell aCell = Cells[x, y];
            int aSelfPathState = aCell.SelfPathState;
            //Debug.LogError($"RefreshPathState, aSelfPathState:{aSelfPathState}");
            aCell.m_PathState = 0;

            if (aSelfPathState == 0)//不可通行
            {    
                return;//不須做額外判斷
            }
            foreach(var aKey in PathStateDic.Keys)
            {
                int aPathState = (int)aKey;
                if((aSelfPathState & aPathState) != 0)//可以往此方向移動
                {
                    var aDir = PathStateDic[aKey];
                    //int aTargetPathState = GetPathState(x + aDir.x, y + aDir.y);
                    if (CheckCanEnter(x + aDir.x, y + aDir.y, aKey))//判斷該方向是否能通行
                    {
                        aCell.m_PathState |= aPathState;//紀錄可通行的方向
                    }
                }
            }
            //Debug.LogError($"RefreshPathState, aCell.m_PathState:{aCell.m_PathState}");
        }
        public override void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            base.ContentOnGUI(iDic);
            DrawPathStateOnGUI(iDic.GetSubDic("DrawPathStateOnGUI"));
        }
        private void DrawPathStateOnGUI(UCL_ObjectDictionary iDic)
        {
            var aGrid = RegionGrid;
            UCL_GUIStyle.PushGUIColor(UCL_Color.Half.Red);
            //using(var aScope = new GUILayout.VerticalScope())
            {
                const float Size = 0.2f;
                const float OffSet = 0.5f * (1f - Size);
                const float Delta = 0.35f;
                for (int y = 0; y < Height; y++)
                {
                    //using (var aScope2 = new GUILayout.HorizontalScope())
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            Cell aCell = Cells[x, y];
                            int aPathState = aCell.m_PathState;
                            foreach (var aKey in PathStateDic.Keys)
                            {
                                int aKeyVal = (int)aKey;
                                if ((aPathState & aKeyVal) != 0)//該方向可通行
                                {
                                    var aDir = PathStateDic[aKey];//方向
                                    var aRect = aGrid.GetCellRect(x + OffSet + Delta * aDir.x, y + OffSet + Delta * aDir.y, Size, Size);

                                    GUI.DrawTexture(aRect, ATS_StaticTextures.White);
                                }
                            }
                            //GUILayout.Box(aCell.m_PathState.ToString("D4"), UCL_GUIStyle.LabelStyle);
                        }
                    }
                }
            }
            UCL_GUIStyle.PopGUIColor();
        }
    }
}

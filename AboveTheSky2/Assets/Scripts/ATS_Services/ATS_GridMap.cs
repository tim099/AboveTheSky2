
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ATS
{

    public class ATS_GridMap : MonoBehaviour
    {
        public Grid m_Grid;
        public Tilemap m_Tilemap;
        public Vector3Int m_TilePosition; // 要檢查的 Tile 位置
        public Transform m_TilePos;
        public TMPro.TextMeshPro m_Info;
        public TileBase m_TileBase;

        public ATS_Unit m_UnitTmp;
        void Start()
        {

        }


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //https://discussions.unity.com/t/camera-main-screentoworldpoint-not-working-as-expected/230672/2
                var cam = Camera.main;
                var camTrans = cam.transform;
                float dist = Vector3.Dot(m_Tilemap.transform.position - camTrans.position, camTrans.forward);
                var mousePosition = Input.mousePosition;
                mousePosition.z = dist;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                m_TilePosition = m_Tilemap.WorldToCell(worldPosition);
                m_Tilemap.SetTile(m_TilePosition, m_TileBase);
                //Debug.LogError($"Down mousePosition:{mousePosition}, worldPosition:{worldPosition}, m_TilePosition:{m_TilePosition}");
                var unit = Instantiate(m_UnitTmp);
                unit.transform.position = m_TilePosition;
            }
            if (Input.GetMouseButtonDown(1))
            {
                var cam = Camera.main;
                var camTrans = cam.transform;
                float dist = Vector3.Dot(m_Tilemap.transform.position - camTrans.position, camTrans.forward);
                var mousePosition = Input.mousePosition;
                mousePosition.z = dist;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                m_TilePosition = m_Tilemap.WorldToCell(worldPosition);
                m_Tilemap.SetTile(m_TilePosition, null);
                //Debug.LogError($"Down mousePosition:{mousePosition}, worldPosition:{worldPosition}, m_TilePosition:{m_TilePosition}");

            }
            {
                string info = string.Empty;
                TileBase tile = m_Tilemap.GetTile<TileBase>(m_TilePosition);
                Vector3 worldPosition = m_Tilemap.CellToWorld(m_TilePosition);
                m_TilePos.position = worldPosition;
                if (tile != null)
                {
                    info = $"tilePosition:{m_TilePosition}, Tile:{tile.name}";

                }
                else
                {
                    info = $"tilePosition:{m_TilePosition}, Tile == null";
                }
                //Debug.Log(info);
                m_Info.text = info;
            }

        }
    }
}
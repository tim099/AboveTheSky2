
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ATS
{

    public class ATS_TestGrid : MonoBehaviour
    {
        public Grid m_Grid;
        public Tilemap m_Tilemap;
        public Vector3Int m_TilePosition; // 要檢查的 Tile 位置
        public Transform m_TilePos;
        public TMPro.TextMeshPro m_Info;
        public TileBase m_TileBase;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = Input.mousePosition;
                mousePosition.z = m_Tilemap.transform.position.z;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                m_TilePosition = m_Tilemap.WorldToCell(worldPosition);
                m_Tilemap.SetTile(m_TilePosition, m_TileBase);
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

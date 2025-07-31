using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Capsule.Core;
using Capsule.UI;

namespace Beastarium
{
    public class BeastariumVisuals : GameplayModule_UIBase
    {
        public BeastariumTable table;
        public int cellWidth = 80;
        private int columnCount;
        private int rowCount;

        [Header("Visuals")]
        public GameObject Explanation;

        public Color singleTint;
        public Color doubleTint;

        [Header("Scollbox Content")]
        public RectTransform scrollBoxContentColumn;
        public RectTransform scrollBoxContentRows;
        public RectTransform scrollBoxContentTable;

        [Header("Column Header")]
        public RectTransform columnHeaderContainer;
        public GameObject columnHeaderGroup;
        public GameObject columnHeader;

        [Header("Row Header")]
        public RectTransform rowHeaderContainer;
        public GameObject rowHeaderGroup;
        public GameObject rowHeader;

        [Header("Table Cells")]
        public RectTransform cellContainer;
        public GameObject cell;
        public CanvasRenderer cellImage;

        [Header("Sprites")]
        public Sprite doublePlus;
        public Sprite plus;
        public Sprite minus;
        public Sprite doubleMinus;

        public override void OnEnter()
        {

        }
        public override void OnExit()
        {

        }

        public override void InitializeUI(int order)
        {
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");

            rowCount = table.enemies.Count;
            columnCount = table.attacks.Count;
            updateSizes();
        }

        private void updateSizes()
        {
            SetSize(scrollBoxContentColumn, columnCount * cellWidth, 2 * cellWidth);
            SetSize(columnHeaderContainer, columnCount * cellWidth, 2 * cellWidth);

            SetSize(scrollBoxContentRows, 2 * cellWidth, rowCount * cellWidth);
            SetSize(rowHeaderContainer, 2 * cellWidth, rowCount * cellWidth);

            SetSize(scrollBoxContentTable, columnCount * cellWidth, rowCount * cellWidth);
            SetSize(cellContainer, columnCount * cellWidth, rowCount * cellWidth);

            fillTableAndHeaders();
        }

        private void SetSize(RectTransform rt, float width, float height)
        {
            if (rt == null) return;
            SetAnchor(rt);
            rt.sizeDelta = new Vector2(width, height);
        }

        private void fillTableAndHeaders()
        {
            fillColumnHeader();
            fillRowHeader();
            fillTable();
        }

        private void fillColumnHeader()
        {
            for (int i = 0; i < columnCount; i++)
            {
                GameObject copy = Instantiate(columnHeader, columnHeaderContainer.transform);
                copy.name = $"ColumnHeader_{i}";

                var rect = copy.GetComponent<RectTransform>();
                SetAnchor(rect);
                rect.anchoredPosition = new Vector2(i * cellWidth, -cellWidth);

                Image colImage = copy.transform.GetChild(0).GetComponent<Image>();
                if (colImage != null && table.enemies[i] != null)
                {
                    colImage.sprite = table.attacks[i].Icon;
                }

                Image bg = copy.GetComponent<Image>();
                if (bg != null && i % 2 == 1)
                {
                    bg.color = singleTint;
                }
            }
        }

        private void fillRowHeader()
        {
            for (int i = 0; i < rowCount; i++)
            {
                GameObject copy = Instantiate(rowHeader, rowHeaderContainer.transform);
                copy.name = $"RowHeader_{i}";

                var rect = copy.GetComponent<RectTransform>();
                SetAnchor(rect);
                rect.anchoredPosition = new Vector2(cellWidth, -i * cellWidth);

                Image rowImage = copy.transform.GetChild(0).GetComponent<Image>();
                if (rowImage != null && table.enemies[i] != null)
                {
                    rowImage.sprite = table.enemies[i].Icon;
                }

                Image bg = copy.GetComponent<Image>();
                if (bg != null && i % 2 == 1)
                {
                    bg.color = singleTint;
                }
            }
        }


        private void fillTable()
        {
            for (int x = 0; x < columnCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    GameObject copy = Instantiate(cell, cellContainer);
                    copy.name = $"Cell_{x}_{y}";

                    var rect = copy.GetComponent<RectTransform>();
                    SetAnchor(rect);
                    rect.anchoredPosition = new Vector2(x * cellWidth, -y * cellWidth);

                    Image cellImage = copy.transform.GetChild(0).GetComponent<Image>();
                    if (cellImage != null)
                    {
                        if ((x % 2 == 1) && (y % 2 == 1)) cellImage.color = doubleTint;
                        else if ((x % 2 == 1) || (y % 2 == 1)) cellImage.color = singleTint;

                        float modifier = table.GetModifier(table.enemies[y], table.attacks[x]);

                        if (modifier == 1f) cellImage.sprite = doublePlus;
                        else if (modifier == 0.5f) cellImage.sprite = plus;
                        else if (modifier == -0.5f) cellImage.sprite = minus;
                        else if (modifier == -1f) cellImage.sprite = doubleMinus;
                        // else use default or no sprite

                    }
                }
            }
        }

        private void SetAnchor(RectTransform rt, float horz = 0, float vert = 1)
        {
            if (rt == null) return;

            Vector2 anchor = new Vector2(horz, vert);
            rt.anchorMin = anchor;
            rt.anchorMax = anchor;
            rt.pivot = anchor;
        }

    }
}

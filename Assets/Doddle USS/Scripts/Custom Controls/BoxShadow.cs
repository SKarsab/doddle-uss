using UnityEngine;
using UnityEngine.UIElements;

namespace doddle.uss
{
    [UxmlElement("Box-Shadow")]
    public partial class BoxShadow : VisualElement
    {
        //Custom style properties for USS
        private static readonly CustomStyleProperty<Color> ColourBoxShadow = new ("--box-shadow-colour");
        private static readonly CustomStyleProperty<int> OffsetX = new CustomStyleProperty<int>("--box-shadow-offset-x");
        private static readonly CustomStyleProperty<int> OffsetY = new CustomStyleProperty<int>("--box-shadow-offset-y");
        private static readonly CustomStyleProperty<float> SpreadX = new CustomStyleProperty<float>("--box-shadow-spread-x");
        private static readonly CustomStyleProperty<float> SpreadY = new CustomStyleProperty<float>("--box-shadow-spread-y");
        private static readonly CustomStyleProperty<int> RadiusCorner = new CustomStyleProperty<int>("--box-shadow-corner-radius");

        private Color boxShadowColour = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        private int xOffset = default(int);
        private int yOffset = default(int);
        private float xSpread = 1.0f;
        private float ySpread = 1.0f;
        private int cornerRadius = default(int);

        //Indicies are in groups of 3 in winding order (clockwise). It doesn't matter what index you start with for a triangle
        //as long as it's clockwise. There are 4 vertices, which when drawn on a 2D plane is broken into 2 triangles each
        // 18-23  5        6  24-29  
        //      \ |        | /
        //    9 - 1--------2 - 10
        //        |        |
        //        |        |
        //        |        |
        //        |        |
        //    8 - 0--------3 - 11
        //      / |        | \
        // 12-17  4        7  30-35
        private Vertex[] vertices = new Vertex[36];
        private static readonly ushort[] indicies = { 0, 8, 9, 0, 9, 1, 1, 5, 6, 1, 6, 2, 2, 10, 11, 2, 11, 3, 3, 7, 4, 3, 4, 0, 0, 1, 2, 0, 2, 3, 0, 4, 12, 0, 12, 13, 0, 13, 14, 0, 14, 15, 0, 15, 16, 0, 16, 17, 0, 17, 8, 1, 9, 18, 1, 18, 19, 1, 19, 20, 1, 20, 21, 1, 21, 22, 1, 22, 23, 1, 23, 5, 2, 6, 24, 2, 24, 25, 2, 25, 26, 2, 26, 27, 2, 27, 28, 2, 28, 29, 2, 29, 10, 3, 11, 30, 3, 30, 31, 3, 31, 32, 3, 32, 33, 3, 33, 34, 3, 34, 35, 3, 35, 7 };

        private const int ZERO = 0;
        private const int CORNER_SAMPLING = 6;

        private const int BL_STARTING_VERTICE = 12;
        private const int TL_STARTING_VERTICE = 18;
        private const int TR_STARTING_VERTICE = 24;
        private const int BR_STARTING_VERTICE = 30;

        private const int BOTTOM_LEFT = 0;
        private const int TOP_LEFT = 1;
        private const int TOP_RIGHT = 2;
        private const int BOTTOM_RIGHT = 3;
        
        private const int BOTTOM_LEFT_HEIGHT = 4;
        private const int TOP_LEFT_HEIGHT = 5;
        private const int TOP_RIGHT_HEIGHT = 6;
        private const int BOTTOM_RIGHT_HEIGHT = 7;

        private const int BOTTOM_LEFT_WIDTH = 8;
        private const int TOP_LEFT_WIDTH = 9;
        private const int TOP_RIGHT_WIDTH = 10;
        private const int BOTTOM_RIGHT_WIDTH = 11;

        [Header("Box Shadow Properties")]
        [Tooltip("Colour of the box shadow fill")]
        [UxmlAttribute("Box-Shadow-Colour")]
        public Color BoxShadowColour
        {
            get => boxShadowColour;
            set => boxShadowColour = value;
        }

        [Tooltip("+ values right, - values left. Moves entire mesh")]
        [UxmlAttribute("X-Offset")]
        public int XOffset
        {
            get => xOffset;
            set => xOffset = value;
        }

        [Tooltip("+ values down, - values up. Moves entire mesh")]
        [UxmlAttribute("Y-Offset")]
        public int YOffset
        {
            get => yOffset;
            set => yOffset = value;
        }

        [Tooltip("Scaling of the shadow in the x direction, both left, and right. Range 1f-100f")]
        [Range(1f, 100f)]
        [UxmlAttribute("X-Spread")]
        public float XSpread
        {
            get => xSpread;
            set => xSpread = value;
        }

        [Tooltip("Scaling of the shadow in the y direction, both down, and up. Range 1f-100f")]
        [Range(1f, 100f)]
        [UxmlAttribute("Y-Spread")]
        public float YSpread
        {
            get => ySpread;
            set => ySpread = value;
        }

        [Tooltip("Radius of corner circle used to round sharp corners. Higher values inc roundedness")]
        [UxmlAttribute("Corner-Radius")]
        public int CornerRadius
        {
            get => cornerRadius;
            set => cornerRadius = value;
        }

        /// <summary>
        /// Box shadow VisualElement background custom control. Width/height will be set to 100% to match parent. 
        /// A wrapper VisualElement is required
        /// </summary>
        /// <remarks>
        /// Inner shape is set to the size of the parent VisualElement - cornerRadius/2 so blur will fade out to the edges of the parent shape
        /// Start and end vertices overlap with 1 and 6 from the corner sampling amount. Only 4 steps from start to end
        /// </remarks>
        public BoxShadow()
        {
            style.width = Length.Percent(100);
            style.height = Length.Percent(100);
            RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
            generateVisualContent += OnGenerateVisualContent;
        }

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(ColourBoxShadow, out Color colour)) { boxShadowColour = colour; }
            if (evt.customStyle.TryGetValue(OffsetX, out int xOffset)) { this.xOffset = xOffset; }
            if (evt.customStyle.TryGetValue(OffsetY, out int yOffset)) { this.yOffset = yOffset; }
            if (evt.customStyle.TryGetValue(SpreadX, out float xSpread)) { this.xSpread = xSpread; }
            if (evt.customStyle.TryGetValue(SpreadY, out float ySpread)) { this.ySpread = ySpread; }
            if (evt.customStyle.TryGetValue(RadiusCorner, out int cornerRadius)) { this.cornerRadius = cornerRadius; }
        }

        /// <summary>
        /// Draws a mesh that sets a single background colour to the size of the parent VisualElement, then draws an outer shape distanced half 
        /// of the corner radius in the x/y directions and rounds those corners with 6 vertices/8 triangles each based off the corner radius 
        /// property. The colour of the outer shape fades to Color.Clear and positions are adjusted by xSpread/ySpread, and xOffset/yOffset.
        /// </summary>
        /// <param name="meshGenerationContext"></param>
        private void OnGenerateVisualContent(MeshGenerationContext meshGenerationContext)
        {
            float circleRadius = (cornerRadius / 2f);

            //Set vertex positions and colours for the inner shape
            vertices[BOTTOM_LEFT].position = new Vector3(circleRadius, contentRect.height - circleRadius, Vertex.nearZ);
            vertices[BOTTOM_LEFT].tint = boxShadowColour;

            vertices[TOP_LEFT].position = new Vector3(circleRadius, circleRadius, Vertex.nearZ);
            vertices[TOP_LEFT].tint = boxShadowColour;

            vertices[TOP_RIGHT].position = new Vector3(contentRect.width - circleRadius, circleRadius, Vertex.nearZ);
            vertices[TOP_RIGHT].tint = boxShadowColour;

            vertices[BOTTOM_RIGHT].position = new Vector3(contentRect.width - circleRadius, contentRect.height - circleRadius, Vertex.nearZ);
            vertices[BOTTOM_RIGHT].tint = boxShadowColour;

            //Set vertex positions and colours for the outer shape height
            vertices[BOTTOM_LEFT_HEIGHT].position = new Vector3(circleRadius, contentRect.height, Vertex.nearZ);
            vertices[BOTTOM_LEFT_HEIGHT].tint = Color.clear;

            vertices[TOP_LEFT_HEIGHT].position = new Vector3(circleRadius, ZERO, Vertex.nearZ);
            vertices[TOP_LEFT_HEIGHT].tint = Color.clear;

            vertices[TOP_RIGHT_HEIGHT].position = new Vector3(contentRect.width - circleRadius, ZERO, Vertex.nearZ);
            vertices[TOP_RIGHT_HEIGHT].tint = Color.clear;

            vertices[BOTTOM_RIGHT_HEIGHT].position = new Vector3(contentRect.width - circleRadius, contentRect.height, Vertex.nearZ);
            vertices[BOTTOM_RIGHT_HEIGHT].tint = Color.clear;

            //Set vertex positions and colours for the outer shape width
            vertices[BOTTOM_LEFT_WIDTH].position = new Vector3(ZERO, contentRect.height - circleRadius, Vertex.nearZ);
            vertices[BOTTOM_LEFT_WIDTH].tint = Color.clear;

            vertices[TOP_LEFT_WIDTH].position = new Vector3(ZERO, circleRadius, Vertex.nearZ);
            vertices[TOP_LEFT_WIDTH].tint = Color.clear;

            vertices[TOP_RIGHT_WIDTH].position = new Vector3(contentRect.width, circleRadius, Vertex.nearZ);
            vertices[TOP_RIGHT_WIDTH].tint = Color.clear;

            vertices[BOTTOM_RIGHT_WIDTH].position = new Vector3(contentRect.width, contentRect.height - circleRadius, Vertex.nearZ);
            vertices[BOTTOM_RIGHT_WIDTH].tint = Color.clear;

            //Set vertex positions and colours for the outer shape corner sampling
            CalculateVerticesAlongArc(BL_STARTING_VERTICE, CORNER_SAMPLING, circleRadius, vertices[BOTTOM_LEFT_HEIGHT].position, vertices[BOTTOM_LEFT_WIDTH].position, vertices[BOTTOM_LEFT].position);
            CalculateVerticesAlongArc(TL_STARTING_VERTICE, CORNER_SAMPLING, circleRadius, vertices[TOP_LEFT_WIDTH].position, vertices[TOP_LEFT_HEIGHT].position, vertices[TOP_LEFT].position);
            CalculateVerticesAlongArc(TR_STARTING_VERTICE, CORNER_SAMPLING, circleRadius, vertices[TOP_RIGHT_HEIGHT].position, vertices[TOP_RIGHT_WIDTH].position, vertices[TOP_RIGHT].position);
            CalculateVerticesAlongArc(BR_STARTING_VERTICE, CORNER_SAMPLING, circleRadius, vertices[BOTTOM_RIGHT_WIDTH].position, vertices[BOTTOM_RIGHT_HEIGHT].position, vertices[BOTTOM_RIGHT].position);

            //Adjust positions for offset and spread
            AdjustVerticePositions();

            MeshWriteData meshWriteData = meshGenerationContext.Allocate(vertices.Length, indicies.Length);
            meshWriteData.SetAllVertices(vertices);
            meshWriteData.SetAllIndices(indicies);
        }

        /// <summary>
        /// Calculates 6 points along an arc in clockwise rotation from the start angle to the end angle
        /// </summary>
        /// <param name="startIndex">Index to start adding to the vertices array</param>
        /// <param name="numVertices">Number of steps from start to end inclusive</param>
        /// <param name="radius">Half the corner radius, raidus of the circle</param>
        /// <param name="startVertex">Outer shape starting vertex</param>
        /// <param name="endVertex">Outer shape ending vertex</param>
        /// <param name="centerVertex">Inner shape vertex</param>
        private void CalculateVerticesAlongArc(int startIndex, int numVertices, float radius, Vector3 startVertex, Vector3 endVertex, Vector3 centerVertex)
        {
            float startAngle = Mathf.Atan2(startVertex.y - centerVertex.y, startVertex.x - centerVertex.x);
            float endAngle = Mathf.Atan2(endVertex.y - centerVertex.y, endVertex.x - centerVertex.x);

            for (int i = 0; i < numVertices; i++)
            {
                int currentVertex = startIndex + i;

                //Unity does winding clockwise for tris, so arc calculations are also clockwise which is opposite of default radian expectations
                //Use this for top left corner
                if (endAngle < startAngle) { endAngle += 2 * Mathf.PI; }
                    
                float angleIncrement = (endAngle - startAngle) / (numVertices - 1);
                float theta = startAngle + i * angleIncrement;

                vertices[currentVertex].position = new Vector3(centerVertex.x + radius * Mathf.Cos(theta), centerVertex.y + radius * Mathf.Sin(theta), Vertex.nearZ);
                vertices[currentVertex].tint = Color.clear;
            }
        }

        /// <summary>
        /// Scale the vertex positions with xSpread/ySpread, and xOffset/yOffset. Start after the inner circle vertices
        /// </summary>
        private void AdjustVerticePositions()
        {
            float xCenter = contentRect.center.x;
            float yCenter = contentRect.center.y;

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].position += new Vector3(xOffset, yOffset, 0);

                if (i > BOTTOM_RIGHT)
                {
                    vertices[i].position.x = (vertices[i].position.x - xCenter) * xSpread + xCenter;
                    vertices[i].position.y = (vertices[i].position.y - yCenter) * ySpread + yCenter;
                }
            }
        }
    }
}
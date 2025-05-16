using UnityEngine;
using UnityEngine.UIElements;

namespace doddle.uss
{
    public class GradientAttribute : PropertyAttribute { }

    [UxmlElement("Gradient")]
    public partial class Gradient : VisualElement
    {
        //Custom style properties for USS
        private static readonly CustomStyleProperty<Color> GradientTopLeft = new CustomStyleProperty<Color>("--gradient-top-left");
        private static readonly CustomStyleProperty<Color> GradientTopRight = new CustomStyleProperty<Color>("--gradient-top-right");
        private static readonly CustomStyleProperty<Color> GradientBottomLeft = new CustomStyleProperty<Color>("--gradient-bottom-left");
        private static readonly CustomStyleProperty<Color> GradientBottomRight = new CustomStyleProperty<Color>("--gradient-bottom-right");

        private Color topLeftColour = default(Color);
        private Color topRightColour = default(Color);
        private Color bottomLeftColour = default(Color);
        private Color bottomRightColour = default(Color);
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

        [Header("Gradient Properties")]
        [Tooltip("Colour that the gradient will transition to in the top left corner")]
        [Gradient, UxmlAttribute("Top-Left-Colour")]
        public Color TopLeftColour
        {
            get => topLeftColour;
            set => topLeftColour = value;
        }

        [Tooltip("Colour that the gradient will transition to in the top right corner")]
        [Gradient, UxmlAttribute("Top-Right-Colour")]
        public Color TopRightColour
        {
            get => topRightColour;
            set => topRightColour = value;
        }

        [Tooltip("Colour that the gradient will transition to in the bottom left corner")]
        [Gradient, UxmlAttribute("Bottom-Left-Colour")]
        public Color BottomLeftColour
        {
            get => bottomLeftColour;
            set => bottomLeftColour = value;
        }

        [Tooltip("Colour that the gradient will transition to in the bottom right corner")]
        [Gradient, UxmlAttribute("Bottom-Right-Colour")]
        public Color BottomRightColour
        {
            get => bottomRightColour;
            set => bottomRightColour = value;
        }

        [Tooltip("Radius of corner circle used to round sharp corners. Higher values inc roundedness")]
        [UxmlAttribute("Corner-Radius")]
        public int CornerRadius
        {
            get => cornerRadius;
            set => cornerRadius = value;
        }

        /// <summary>
        /// Gradient VisualElement background custom control. Width/height will be set to 100% to match parent. 
        /// A wrapper VisualElement is required
        /// </summary>
        /// <remarks>
        /// Transition step locked at 50% with current solution.
        /// </remarks>
        public Gradient()
        {
            style.width = Length.Percent(100);
            style.height = Length.Percent(100);
            RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
            generateVisualContent += OnGenerateVisualContent;
        }

        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetValue(GradientBottomLeft, out Color blColour)) { bottomLeftColour = blColour; }
            if (evt.customStyle.TryGetValue(GradientTopLeft, out Color tlColour)) { topLeftColour = tlColour; }
            if (evt.customStyle.TryGetValue(GradientTopRight, out Color trColour)) { topRightColour = trColour; }
            if (evt.customStyle.TryGetValue(GradientBottomRight, out Color brColour)) { bottomRightColour = brColour; }
        }

        /// <summary>
        /// Draws a simple mesh that sets a gradient background on the current VisualElement. Current solution only has 4 
        /// vertices, with no support for border radius
        /// </summary>
        /// <param name="meshGenerationContext">MeshGenerationContext</param>
        private void OnGenerateVisualContent(MeshGenerationContext meshGenerationContext)
        {
            float circleRadius = (cornerRadius / 2f);

            //Set vertex positions and colours for the inner shape
            vertices[BOTTOM_LEFT].position = new Vector3(circleRadius, contentRect.height - circleRadius, Vertex.nearZ);
            vertices[BOTTOM_LEFT].tint = bottomLeftColour;

            vertices[TOP_LEFT].position = new Vector3(circleRadius, circleRadius, Vertex.nearZ);
            vertices[TOP_LEFT].tint = topLeftColour;

            vertices[TOP_RIGHT].position = new Vector3(contentRect.width - circleRadius, circleRadius, Vertex.nearZ);
            vertices[TOP_RIGHT].tint = topRightColour;

            vertices[BOTTOM_RIGHT].position = new Vector3(contentRect.width - circleRadius, contentRect.height - circleRadius, Vertex.nearZ);
            vertices[BOTTOM_RIGHT].tint = bottomRightColour;

            //Set vertex positions and colours for the outer shape height
            vertices[BOTTOM_LEFT_HEIGHT].position = new Vector3(circleRadius, contentRect.height, Vertex.nearZ);
            vertices[BOTTOM_LEFT_HEIGHT].tint = bottomLeftColour;

            vertices[TOP_LEFT_HEIGHT].position = new Vector3(circleRadius, ZERO, Vertex.nearZ);
            vertices[TOP_LEFT_HEIGHT].tint = topLeftColour;

            vertices[TOP_RIGHT_HEIGHT].position = new Vector3(contentRect.width - circleRadius, ZERO, Vertex.nearZ);
            vertices[TOP_RIGHT_HEIGHT].tint = topRightColour;

            vertices[BOTTOM_RIGHT_HEIGHT].position = new Vector3(contentRect.width - circleRadius, contentRect.height, Vertex.nearZ);
            vertices[BOTTOM_RIGHT_HEIGHT].tint = bottomRightColour;

            //Set vertex positions and colours for the outer shape width
            vertices[BOTTOM_LEFT_WIDTH].position = new Vector3(ZERO, contentRect.height - circleRadius, Vertex.nearZ);
            vertices[BOTTOM_LEFT_WIDTH].tint = bottomLeftColour;

            vertices[TOP_LEFT_WIDTH].position = new Vector3(ZERO, circleRadius, Vertex.nearZ);
            vertices[TOP_LEFT_WIDTH].tint = topLeftColour;

            vertices[TOP_RIGHT_WIDTH].position = new Vector3(contentRect.width, circleRadius, Vertex.nearZ);
            vertices[TOP_RIGHT_WIDTH].tint = topRightColour;

            vertices[BOTTOM_RIGHT_WIDTH].position = new Vector3(contentRect.width, contentRect.height - circleRadius, Vertex.nearZ);
            vertices[BOTTOM_RIGHT_WIDTH].tint = bottomRightColour;

            //Set vertex positions and colours for the outer shape corner sampling
            CalculateVerticesAlongArc(BL_STARTING_VERTICE, CORNER_SAMPLING, circleRadius, vertices[BOTTOM_LEFT_HEIGHT].position, vertices[BOTTOM_LEFT_WIDTH].position, vertices[BOTTOM_LEFT].position);
            CalculateVerticesAlongArc(TL_STARTING_VERTICE, CORNER_SAMPLING, circleRadius, vertices[TOP_LEFT_WIDTH].position, vertices[TOP_LEFT_HEIGHT].position, vertices[TOP_LEFT].position);
            CalculateVerticesAlongArc(TR_STARTING_VERTICE, CORNER_SAMPLING, circleRadius, vertices[TOP_RIGHT_HEIGHT].position, vertices[TOP_RIGHT_WIDTH].position, vertices[TOP_RIGHT].position);
            CalculateVerticesAlongArc(BR_STARTING_VERTICE, CORNER_SAMPLING, circleRadius, vertices[BOTTOM_RIGHT_WIDTH].position, vertices[BOTTOM_RIGHT_HEIGHT].position, vertices[BOTTOM_RIGHT].position);

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
                ApplyCornerColours(currentVertex);
            }
        }

        /// <summary>
        /// Sets corner radius vertices along the arc to match colours selected in the colour pickers in editor
        /// </summary>
        /// <param name="currentVertex"></param>
        private void ApplyCornerColours(int currentVertex)
        {
            if (currentVertex < TL_STARTING_VERTICE)
            {
                vertices[currentVertex].tint = bottomLeftColour;
                return;
            }

            if (currentVertex >= TL_STARTING_VERTICE && currentVertex < TR_STARTING_VERTICE)
            {
                vertices[currentVertex].tint = topLeftColour;
                return;
            }

            if (currentVertex >= TR_STARTING_VERTICE && currentVertex < BR_STARTING_VERTICE)
            {
                vertices[currentVertex].tint = topRightColour;
                return;
            }

            if (currentVertex >= BR_STARTING_VERTICE)
            {
                vertices[currentVertex].tint = bottomRightColour;
                return;
            }
        }
    }
}
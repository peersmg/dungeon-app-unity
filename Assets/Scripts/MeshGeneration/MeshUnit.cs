public class MeshUnit
{
    int topLeftVertexIndex;
    int topRightVertexIndex;
    int bottomLeftVertexIndex;
    int bottomRightVertexIndex;

    MeshUnit(int topLeftVertexIndex, int topRightVertexIndex, int bottomLeftVertexIndex, int bottomRightVertexIndex)
    {
        this.topLeftVertexIndex = topLeftVertexIndex;
        this.topRightVertexIndex = topRightVertexIndex;
        this.bottomLeftVertexIndex = bottomLeftVertexIndex;
        this.bottomRightVertexIndex = bottomRightVertexIndex;
    }
}
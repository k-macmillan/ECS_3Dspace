using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Space
{
    /// <summary>
    /// Recursive class that manages collisions and nearest ship
    /// </summary>
    public class BlockSpace
    {
        private const int SpaceSize = 729;          // 9x9x9 blocks
        private const int BlocksPerChunk = 125;     // 5x5x5 blocks
        private const int N = 9;                    // Blocks per side
        private const int Chunks = 8;               // There are 8: 5x5x5 chunks
        private Block[] blocks;
        
        public void SetBlockSpace(float3 Lower, float3 Upper, NativeArray<Entity> Entities)
        {
            float3 increment = new float3((Upper - Lower) / N);
            for (int i = 0; i < SpaceSize; ++i)
            {
                blocks[i] = new Block(Lower + (i * increment), increment);
            }
        }
    }

    public class Chunk
    {

    }

    public class Block
    {
        private float3 lower;
        private float3 upper;
        public NativeArray<Entity> entities;

        public Block(float3 Lower, float3 Increment)
        {
            lower = Lower;
            upper = Lower + Increment;
            entities = new NativeArray<Entity>();
        }
    }
}
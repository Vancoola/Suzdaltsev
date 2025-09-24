using Suzdaltsev.Domain;
using Suzdaltsev.Domain.Interfaces;

namespace Suzdaltsev.Data.Repositories;

public class NodeRepo : INodeRepo
{
    public Node Parent { get; } = Node.Create("");

    public IEnumerable<string> FindAdvertisers(string[] segments, int depth = 0)
    {
        var result = Parent.FindAdvertisers(segments, depth);
        return result;
    }

    public void Create(string[] segments, string advertiser)
    {
        foreach (var segmentAll in segments)
        {
            var segment = segmentAll.Split('/', StringSplitOptions.RemoveEmptyEntries).Reverse().ToArray();
            Parent.CreateTree(segment, segment.Length-1, advertiser);
        }
    }
    
}
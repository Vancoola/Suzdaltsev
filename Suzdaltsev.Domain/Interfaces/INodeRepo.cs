namespace Suzdaltsev.Domain.Interfaces;

public interface INodeRepo
{
    
    //ConcurrentList<Node> Nodes { get; }
    Node Parent { get; }
    IEnumerable<string> FindAdvertisers(string[] segments, int depth = 0);
    void Create(string[] segments, string advertiser);

}
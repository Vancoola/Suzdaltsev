using System.Collections.Concurrent;

namespace Suzdaltsev.Domain;

public class Node
{
    private ConcurrentDictionary<string, Node> _children;
    private HashSet<string> _advertisers;
    
    private Node(string segment)
    {
        Segment = segment;
        _advertisers = new HashSet<string>();
        _children = new ConcurrentDictionary<string, Node>();
    }
    
    public string Segment { get; init; }
    public IReadOnlySet<string> Advertisers => _advertisers;
    
    public IReadOnlyDictionary<string, Node> Children => _children;
    public Node AddChild(string segment, Node node)
    {
        _children.TryAdd(segment, node);
        return node;
    }
    public void AddAdvertiser(string advertiser) => _advertisers.Add(advertiser);

    public Node GetOrAddChild(string segment) 
        => _children.TryGetValue(segment, out var node) ? node : AddChild(segment, Create(segment));

    public IEnumerable<string> FindAdvertisers(string[] segments, int depth = 0)
    {
        var result = new HashSet<string>(Advertisers);
        if (depth <= segments.Length-1 && Children.TryGetValue(segments[depth], out var child))
        {
            if(child.Segment != segments.Last())
                result.UnionWith(child.FindAdvertisers(segments, depth + 1));
            result.UnionWith(child.Advertisers);
        }
        return result;
    }
    
    public IEnumerable<string> FindAdvertisers(string location, int depth = 0)
    {
        var result = new HashSet<string>(Advertisers);
        if (depth < Segment.Length-1 && Children.TryGetValue(location, out var child))
        {
            result.UnionWith(child.FindAdvertisers(location, depth + 1));
        }
        return result;
    }

    public (Node?, int) Find(string location, int depth = 0)
    {
        (Node? node, int index) result = (null, -1);
        if (depth < Segment.Length - 1 && Children.TryGetValue(location, out var child))
        {
            result = child.Find(location, depth + 1);
        }
        return result;
    }
    
    public void CreateTree(string[] segments, int depth, string advertiser)
    {
        
        if(depth < 0)
            return;
        
        var segment = segments[depth];

        var child = GetOrAddChild(segment);
        
        if(segment == segments[0])
            child.AddAdvertiser(advertiser);
        
        //node.AddAdvertiser(advertiser);
        child.CreateTree(segments, depth - 1, advertiser);
    }
    
    public static Node Create(string segment) => new Node(segment);
    
}
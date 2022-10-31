namespace ComputerService.Models;
public class Link
{
    public Uri Href { get; set; }
    public string Rel { get; set; }
    public string Method { get; set; }

    public Link()
    {

    }

    public Link(Uri href, string rel)
    {
        Href = href;
        Rel = rel;
        Method = null;
    }

    public Link(Uri href, string rel, string method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }
}
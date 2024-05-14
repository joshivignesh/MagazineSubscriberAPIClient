public class TokenData()
{
    public bool success { get; set; }
    public string token { get; set; }
}

public class Magazines
{
    public magazinesData[] data { get; set; }
}

public class magazinesData
{
    public int id { get; set; }
    public string name { get; set; }
    public string category { get; set; }
}


public class Categories()
{
    public string[] data { get; set; }
}
public class CategoriesSubscribed()
{
    public string id { get; set; }
    public string data { get; set; }
    public bool IsSubscribed { get; set; }
}


public class Subscriber
{
    public SubscriberData[] data { get; set; }
    public string token { get; set; }
    public bool IsSubscribed { get; set; }
}

public class SubscriberData
{
    public string id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int[] magazineIds { get; set; }
}
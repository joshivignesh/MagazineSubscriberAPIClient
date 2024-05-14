using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Magazine Subscriber");

await ProcessRepositoriesAsync(client);

static async Task ProcessRepositoriesAsync(HttpClient client)
{
    string baseUrl = "http://magazinestore.azurewebsites.net";
    var response = await client.GetStringAsync($"{baseUrl}/api/token");
    TokenData? tokenData = JsonSerializer.Deserialize<TokenData>(response);

    if (response.Length > 0)
    {
        if (tokenData?.success == true)
        {
            var token = tokenData.token;

            Console.WriteLine($"GET {baseUrl}/api/categories/{token}");

            var responseCategory = await client.GetStringAsync($"{baseUrl}/api/categories/{token}");
            Categories? categories = JsonSerializer.Deserialize<Categories>(responseCategory);

            Console.WriteLine($"GET {baseUrl}/api/subscribers/{token}");

            var subscriberDetails = await client.GetStringAsync($"{baseUrl}/api/subscribers/{token}");
            Subscriber? subscriber = JsonSerializer.Deserialize<Subscriber>(subscriberDetails);
            var categoriesSubscribed = new List<CategoriesSubscribed>();
            for (int categorycnt = 0; categorycnt < categories?.data.Length; categorycnt++)
            {
                Console.WriteLine($"GET {baseUrl}/api/magazines/{token}/{categories.data[categorycnt]}");

                var responseCategoryMagazine = await client.GetStringAsync($"{baseUrl}/api/magazines/{token}/{categories?.data[categorycnt]}");
                Magazines? magazines = JsonSerializer.Deserialize<Magazines>(responseCategoryMagazine);
                foreach (var subs in subscriber?.data)
                {
                    foreach (var subscriberData in subs?.magazineIds)
                    {
                        foreach (var magazine in magazines.data)
                        {
                            if (subscriberData == magazine?.id)
                            {
                                subscriber.IsSubscribed = true;
                                categoriesSubscribed.Add(new CategoriesSubscribed()
                                { id = subs.id, data = categories?.data[categorycnt], IsSubscribed = true });
                                goto Found;
                            }
                        }
                    }
                Found:
                    Console.WriteLine($"Found subscriber {subs.id} for {categories?.data[categorycnt]}");
                }
            }
            var lstUserAllCategory = categoriesSubscribed.Where(scw => scw.IsSubscribed).Select(sc => sc.id).ToList().Distinct();
            JsonContent content = JsonContent.Create(lstUserAllCategory);
            Console.WriteLine($"POST {baseUrl}/api/answer/{token}");
            var SubscriberPost = await client.PostAsync($"{baseUrl}/api/answer/{token}", content);
            if (SubscriberPost.IsSuccessStatusCode)
            {
                Console.WriteLine("All Subscribers returned successfully");
            }
        }
    }
}
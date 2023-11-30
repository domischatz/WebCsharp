namespace Web_Grundlagen
{
public static class SessionExtensions {

public static void SetObject(this ISession session, string key, object value)
{
    session.SetString(key, JsonConvert.SerializeObject(value));
}

public static T GetObject<T>(this ISession session, string key)
{
    var value = session.GetString(key);
    return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
} }
}
//https://stackoverflow.com/questions/54437384/is-there-a-way-to-use-session-for-objects-in-asp-net-core-like-in-web-forms
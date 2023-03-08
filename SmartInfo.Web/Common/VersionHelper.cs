using System.Reflection;

namespace SmartInfo.Web.Common;

public static class VersionHelper
{
    private const string VersionQueryParameter = "v";

    private static readonly Version Version =
        Assembly.GetExecutingAssembly().GetName().Version;

    private static string GetVersionToken()
    {
        return VersionQueryParameter + "=" + Version.ToString(2);
    }

    public static string AddVersionToken(string url)
    {
        var versionToken = GetVersionToken();
        var queryStart = url.IndexOf("?", StringComparison.Ordinal);
        url = queryStart == -1
            ? url + "?" + versionToken
            : url.Insert(queryStart + 1, versionToken + "&");

        return url;
    }
}
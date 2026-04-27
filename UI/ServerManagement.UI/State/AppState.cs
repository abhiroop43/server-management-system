using ServerManagement.UI.Models;

namespace ServerManagement.UI.State;

public class AppState
{
    private UserPreferences? UserPreferences { get; set; }
    private string? UserAuthToken { get; set; }
    private DateTime? AuthTokenExpires { get; set; }

    public void SetUserPreferences(UserPreferences userPreferences)
    {
        UserPreferences = userPreferences;
        // save to backend
    }

    public UserPreferences? GetUserPreferences()
    {
        // read from backend
        return UserPreferences;
    }

    public bool IsUserLoggedIn()
    {
        if (AuthTokenExpires == null)
            return false;

        return !(AuthTokenExpires <= DateTime.UtcNow);
    }

    public void SetAuthToken(string authToken, DateTime authTokenExpires)
    {
        UserAuthToken = authToken;
        AuthTokenExpires = authTokenExpires;
    }
}

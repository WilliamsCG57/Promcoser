using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Core.Settings;

namespace PromcoserDOMAIN.Core.Interfaces
{
    public interface IJWTService
    {
        JWTSettings _settings { get; }

        string GenerateJWToken(Personal personal);
    }
}
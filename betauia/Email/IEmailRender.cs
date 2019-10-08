using System.Threading.Tasks;

namespace betauia.Email
{
    public interface IEmailRender
    {
        Task<string> RenderViewToStringAsync<Tmodel>(string viewname, Tmodel model);
    }
}
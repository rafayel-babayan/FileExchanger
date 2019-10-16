using System.Threading.Tasks;

namespace FileExchanger.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}

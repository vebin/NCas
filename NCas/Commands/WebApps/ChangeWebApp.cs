using ENode.Commanding;

namespace NCas.Commands.WebApps
{
    /// <summary>删除WebApp命令
    /// </summary>
    public class ChangeWebApp : Command<string>
    {
        public int UseFlag { get; set; }

        public ChangeWebApp()
        {
            
        }

        public ChangeWebApp(string id, int useFlag) : base(id)
        {
            UseFlag = useFlag;
        }
    }
}

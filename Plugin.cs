using Exiled.API.Features;
using Exiled.CreditTags;

public class Plugin : Plugin<Config>
{
    public override string Name => "滚木插件";
    public override string Author => "青枫社区服主";
    public override void OnEnabled()
    {
        Log.Info("加载成功！");
        // 如果需要在玩家加入时显示信息，请在此处注册事件或调用相应方法，例如：
        // PlayerEvent.Joining += OnPlayerJoining;
        base.OnEnabled();
    }
    public override void OnDisabled()
    {
        Log.Info("卸载成功！");
        base.OnDisabled();
    }
}

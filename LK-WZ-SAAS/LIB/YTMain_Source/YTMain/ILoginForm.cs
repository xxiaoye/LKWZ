namespace YTMain
{
    using System;
    using System.Windows.Forms;
    using YtClient;
    using YtClient.data;
    using YtMain;
    using YtSys;

    public interface ILoginForm
    {
        Control GetControl();
        void InitUser(Sys ui);
        void LoginAction(ActionLoad ac);
        void LoginFai(ServiceMsg msg);
        void LoginSuc(ServiceMsg msg);
        void SetUMain(UMain main);
    }
}


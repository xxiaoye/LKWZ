namespace YTMain
{
    using System;
    using System.Runtime.CompilerServices;

    public class YwAlert
    {
        public YwAlert(string url, string msg)
        {
            this.Url = url;
            this.Msg = msg;
            this.Right = 0;
        }

        public YwAlert(string url, string msg, int r)
        {
            this.Url = url;
            this.Msg = msg;
            this.Right = r;
        }

        public YwAlert(string url, string msg, string api)
        {
            this.Url = url;
            this.Msg = msg;
            this.Api = api;
            this.Right = 0;
        }

        public YwAlert(string url, string msg, string api, int r)
        {
            this.Url = url;
            this.Msg = msg;
            this.Api = api;
            this.Right = r;
        }

        public string Api { get; set; }

        public string Msg { get; set; }

        public int Right { get; set; }

        public string Url { get; set; }
    }
}


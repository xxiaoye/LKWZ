namespace YTMain
{
    using System;
    using System.Runtime.CompilerServices;

    public class UrlLink
    {
        public UrlLink(string t, string u)
        {
            this.Text = t;
            this.Url = u;
        }

        public string Text { get; set; }

        public string Url { get; set; }
    }
}


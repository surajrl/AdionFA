﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Adion.FA.UI.Station.Modules.Trader.Model
{
    public class Link
    {
        public Link(string text, string url)
            : this(text, url, url)
        {
            Text = text;
            Url = url;
        }

        public Link(string text, string display, string url)
        {
            Text = text;
            Display = display;
            Url = url;
        }


        public string Text { get; }

        public string Url { get; }

        public string Display { get; }
    }
}

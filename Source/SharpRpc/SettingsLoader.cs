﻿#region License
/*
Copyright (c) 2013 Daniil Rodin, Maxim Sannikov of Buhgalteria.Kontur team of SKB Kontur

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharpRpc
{
    public class SettingsLoader : ISettingsLoader
    {
        private readonly string hostSettingsPath;
        private readonly Func<string, string> getServiceSettingsPath;
        private readonly Encoding encoding;
        private readonly IHostSettingsParser hostSettingsParser;
        private readonly IServiceSettingsParser serviceSettingsParser;

        public SettingsLoader(string hostSettingsPath, Func<string, string> getServiceSettingsPath, Encoding encoding, 
            IHostSettingsParser hostSettingsParser, IServiceSettingsParser serviceSettingsParser)
        {
            this.hostSettingsPath = hostSettingsPath;
            this.getServiceSettingsPath = getServiceSettingsPath;
            this.encoding = encoding;
            this.hostSettingsParser = hostSettingsParser;
            this.serviceSettingsParser = serviceSettingsParser;
        }

        public IHostSettings LoadHostSettings()
        {
            return hostSettingsParser.Parse(File.ReadAllText(hostSettingsPath, encoding));
        }

        public IReadOnlyDictionary<string, string> GetServiceSettings(string serviceName)
        {
            return serviceSettingsParser.Parse(File.ReadAllText(getServiceSettingsPath(serviceName), encoding));
        }
    }
}
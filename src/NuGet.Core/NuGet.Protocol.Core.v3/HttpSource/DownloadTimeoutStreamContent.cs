// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Net.Http;

namespace NuGet.Protocol
{
    public class DownloadTimeoutStreamContent : StreamContent
    {
        public DownloadTimeoutStreamContent(string downloadName, Stream networkStream, TimeSpan timeout)
            : base(new DownloadTimeoutStream(downloadName, networkStream, timeout))
        {
        }
    }
}

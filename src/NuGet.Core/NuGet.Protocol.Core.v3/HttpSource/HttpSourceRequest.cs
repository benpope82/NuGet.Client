// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using System.Threading;
using NuGet.Common;

namespace NuGet.Protocol
{
    /// <summary>
    /// A non-cached HTTP request handled by <see cref="HttpSource"/>.
    /// </summary>
    public class HttpSourceRequest
    {
        public static readonly TimeSpan DefaultRequestTimeout = TimeSpan.FromSeconds(100);

        public HttpSourceRequest(string uri, ILogger log)
            : this(
                uri,
                () => HttpRequestMessageFactory.Create(HttpMethod.Get, uri, log))
        {
        }

        public HttpSourceRequest(Uri uri, ILogger log)
            : this(
                uri?.ToString(),
                () => HttpRequestMessageFactory.Create(HttpMethod.Get, uri, log))
        {
        }

        public HttpSourceRequest(Uri uri, Func<HttpRequestMessage> requestFactory)
            : this(uri?.ToString(), requestFactory)
        {
        }

        public HttpSourceRequest(string uri, Func<HttpRequestMessage> requestFactory)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (requestFactory == null)
            {
                throw new ArgumentNullException(nameof(requestFactory));
            }

            Uri = uri;
            RequestFactory = requestFactory;
            IgnoreNotFounds = false;
            RequestTimeout = DefaultRequestTimeout;
            DownloadTimeout = HttpRetryHandlerRequest.DefaultDownloadTimeout;
        }

        /// <summary>
        /// The URI that is bing requested by the <see cref="RequestFactory"/>. This value is only
        /// used for messages intented for the user or log messages.
        /// </summary>
        public string Uri { get; }
        
        /// <summary>
        /// A factory that can be called repeated to build the HTTP request message.
        /// </summary>
        public Func<HttpRequestMessage> RequestFactory { get; }

        /// <summary>
        /// When processing the <see cref="HttpResponseMessage"/>, this flag allows
        /// <code>404 Not Found</code> to be interpreted as a null response.
        /// </summary>
        public bool IgnoreNotFounds { get; set; }

        /// <summary>
        /// The timeout to use when fetching the <see cref="HttpResponseMessage"/>. Since
        /// <see cref="HttpSource"/> only uses <see cref="HttpCompletionOption.ResponseHeadersRead"/>,
        /// this means that we wait this amount of time for only the HTTP headers to be returned.
        /// Downloading the response body is not included in this timeout.
        /// </summary>
        public TimeSpan RequestTimeout { get; set; }

        /// <summary>The timeout to apply to <see cref="DownloadTimeoutStream"/> instances.</summary>
        public TimeSpan DownloadTimeout { get; set; }

        /// <summary>The semaphore used to limit the concurrently of HTTP requests.</summary>
        public SemaphoreSlim Semaphore { get; set; }
    }
}

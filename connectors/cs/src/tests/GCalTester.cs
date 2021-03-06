/* Copyright (c) 2008 Google Inc. All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Text;

using Google.GData.Client;
using Google.GData.Calendar;
using Google.GCalExchangeSync.Library.Util;

using Google.GCalExchangeSync.Library;

namespace Google.GCalExchangeSync.Tests.Diagnostics
{
    /// <summary>
    /// Class to throttle connections made to Google, if failures occur.
    /// </summary>
    public class FakeThrottle : IConnectionThrottle
    {
        /// <summary>
        /// Create a fake throttler
        /// </summary>
        public FakeThrottle()
        {
        }

        /// <summary>
        /// Report a success to the throttler. Should be called after successfully getting a feed.
        /// </summary>
        /// <returns>void</returns>
        public void ReportSuccess()
        {
        }

        /// <summary>
        /// Report a failure to the throttler. Should be called after failing to get a feed.
        /// </summary>
        /// <returns>void</returns>
        public void ReportFailure()
        {
        }

        /// <summary>
        /// Ask the throttler to wait as necessary before new connection.
        /// Should be called before asking for a feed.
        /// </summary>
        /// <returns>void</returns>
        public void WaitBeforeNewConnection()
        {
        }
    };

    public class GCalTester
    {
        public static EventFeed QueryGCalFreeBusy(
            string gcalUserEmail)
        {
            IConnectionThrottle throttle = new FakeThrottle();
            GCalGateway gw = new GCalGateway(ConfigCache.GoogleAppsLogin,
                                             ConfigCache.GoogleAppsPassword,
                                             ConfigCache.GoogleAppsDomain,
                                             throttle);

            DateTime now = DateTime.Now;
            DateTime start = now.AddDays(-7);
            DateTime end = now.AddDays(+7);
            DateTimeRange range = new DateTimeRange(start, end);

            EventFeed feed = gw.QueryGCal(gcalUserEmail,
                                          GCalVisibility.Private,
                                          GCalProjection.FreeBusy,
                                          DateTime.MinValue,
                                          range);
            return feed;
        }
    }
}

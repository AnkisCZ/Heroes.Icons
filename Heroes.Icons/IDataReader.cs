﻿using System.Collections.Generic;

namespace Heroes.Icons
{
    public interface IDataReader
    {
        /// <summary>
        /// Gets a collection of all the ids.
        /// </summary>
        IEnumerable<string> GetIds { get; }

        /// <summary>
        /// Gets a collection of all the names.
        /// </summary>
        IEnumerable<string> GetNames { get; }

        /// <summary>
        /// Gets a collection of all hyperlink ids.
        /// </summary>
        IEnumerable<string> GetHyperlinkIds { get; }

        /// <summary>
        /// Get the amount of total items.
        /// </summary>
        int Count { get; }
    }
}

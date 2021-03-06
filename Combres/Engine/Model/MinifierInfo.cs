﻿#region License
// Copyright 2009-2015 Buu Nguyen
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at https://github.com/buunguyen/combres
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Combres
{
    /// <summary>
    /// Contains the metadata for an <see cref="IResourceMinifier"/>.
    /// </summary>
    public sealed class MinifierInfo
    {
        /// <summary>
        /// The name of this minifier
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The full-qualified type name of the minifier
        /// </summary>
        public Type Type { get; internal set; }

        /// <summary>
        /// The full-qualified type name of the binder used for the minifier
        /// </summary>
        public Type BinderType { get; internal set; }

        /// <summary>
        /// Configuration parameters to initialize the minifier
        /// </summary>
        public IList<XElement> Parameters { get; internal set; }

        /// <summary>
        /// Returns true if both minifiers share the same <see cref="Name"/>, <see cref="Type"/>, <see cref="BinderType"/>, and
        /// <see cref="Parameters"/>.
        /// </summary>
        /// <param name="obj">The minifier to be compared.</param>
        /// <returns>True if both minifiers share the same <see cref="Name"/>, <see cref="Type"/>, <see cref="BinderType"/>, and
        /// <see cref="Parameters"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            var other = obj as MinifierInfo;
            if (other == null)
                return false;
            return Name == other.Name &&
                   Type == other.Type &&
                   BinderType == other.BinderType &&
                   Parameters.IsEqualTo(other.Parameters);
        }

        /// <remark>
        /// Guarantee to return same hashcode for identical minifier across multiple runs.
        /// </remark>
        public override int GetHashCode()
        {
            var equalityComparer = new XNodeEqualityComparer();
            var factors = new List<int>();
            factors.AddRange(new[] { Name, Type.ToString(), BinderType.ToString() }.Select(o => o.GetHashCode()));
            factors.AddRange(Parameters.Select(p => equalityComparer.GetHashCode(p)));
            return factors.Aggregate(17, (accum, element) => 31 * accum + element);
        }
    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{name}
	/// </pre>
	/// neither parameter is optional 
	/// </summary>
	public class IndexNamePathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor>
		where TDescriptor : IndexNamePathDescriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		internal string _Name { get; set; }
		
		public TDescriptor Index<TAlternative>() where TAlternative : class
		{
			this._Index = typeof(TAlternative);
			return (TDescriptor)this;
		}
			
		public TDescriptor Index(string indexType)
		{
			this._Index = indexType;
			return (TDescriptor)this;
		}

		public TDescriptor Index(Type indexType)
		{
			this._Index = indexType;
			return (TDescriptor)this;
		}
		
		public TDescriptor Name(string name)
		{
			this._Name = name;
			return (TDescriptor)this;
		}

		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			if (this._Name == null)
				throw new DslException("missing Repository()");
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName(this._Index) ?? inferrer.DefaultIndex; 
			var pathInfo = base.ToPathInfo(queryString);
			pathInfo.Index = index;
			pathInfo.Name = this._Name;
			return pathInfo;
		}

	}
}

﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndicesOperationResponse CreateIndex(Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector)
		{
			var descriptor = createIndexSelector(new CreateIndexDescriptor(this._connectionSettings)); 
			return this.Dispatch<CreateIndexDescriptor, CreateIndexRequestParameters, IndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesCreateDispatch<IndicesOperationResponse>(p, d._IndexSettings)
			);
		} 

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> CreateIndexAsync(Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector)
		{
			var descriptor = createIndexSelector(new CreateIndexDescriptor(this._connectionSettings)); 
			return this.DispatchAsync
				<CreateIndexDescriptor, CreateIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					descriptor,
					(p, d) => this.RawDispatch.IndicesCreateDispatchAsync<IndicesOperationResponse>(p, d._IndexSettings)
				);
		}
	}
}
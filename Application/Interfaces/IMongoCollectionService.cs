using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace IdentityService.Application.Interfaces;

public interface IMongoCollectionService
{

    Task InsertOneAsync<TDocument>(TDocument document, InsertOneOptions options = null, CancellationToken cancellationToken = default) where TDocument : class;
    Task InsertManyAsync<TDocument>(IEnumerable<TDocument> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default);
    Task<UpdateResult> UpdateOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
    Task<UpdateResult> UpdateOneAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
    //
    // Summary:
    //     Updates many documents.
    //
    // Parameters:
    //   filter:
    //     The filter.
    //
    //   update:
    //     The update.
    //
    //   options:
    //     The options.
    //
    //   cancellationToken:
    //     The cancellation token.
    //
    // Returns:
    //     The result of the update operation.
    Task<UpdateResult> UpdateManyAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

    //
    // Summary:
    //     Updates many documents.
    //
    // Parameters:
    //   session:
    //     The session.
    //
    //   filter:
    //     The filter.
    //
    //   update:
    //     The update.
    //
    //   options:
    //     The options.
    //
    //   cancellationToken:
    //     The cancellation token.
    //
    // Returns:
    //     The result of the update operation.
    Task<UpdateResult> UpdateManyAsync<TDocument>(IClientSessionHandle session, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

    IFindFluent<TDocument, TDocument> Find<TDocument>(Expression<Func<TDocument, bool>> filter, FindOptions options = null);
    IFindFluent<TDocument, TDocument> Find<TDocument>(FilterDefinition<TDocument> filter, FindOptions options = null);

    Task<DeleteResult> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default);

    Task<long> CountDocumentsAsync<TDocument>(FilterDefinition<TDocument> filter, CountOptions options = null, CancellationToken cancellationToken = default);
    Task<DeleteResult> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default(CancellationToken));
    Task<BulkWriteResult<TDocument>> BulkWriteAsync<TDocument>(IEnumerable<WriteModel<TDocument>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

}
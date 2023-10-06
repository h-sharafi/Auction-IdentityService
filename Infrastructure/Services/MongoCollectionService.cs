using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityService.Application.Interfaces;
using MongoDB.Driver;

namespace IdentityService.Infrastructure.Services;

public class MongoCollectionService: IMongoCollectionService
{
private readonly ICollectionService _collectionService;

    public MongoCollectionService(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    public async Task<BulkWriteResult<TDocument>> BulkWriteAsync<TDocument>(IEnumerable<WriteModel<TDocument>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    return await _mongoCollection.BulkWriteAsync(requests, options, cancellationToken);
}


public async Task<long> CountDocumentsAsync<TDocument>(FilterDefinition<TDocument> filter, CountOptions options = null, CancellationToken cancellationToken = default)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    return await _mongoCollection.CountDocumentsAsync(filter, options, cancellationToken);
}

public async Task<DeleteResult> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    return await _mongoCollection.DeleteOneAsync(filter, cancellationToken);
}


public async Task<DeleteResult> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    return await _mongoCollection.DeleteOneAsync(filter, cancellationToken);
}

public IFindFluent<TDocument, TDocument> Find<TDocument>(Expression<Func<TDocument, bool>> filter, FindOptions options = null)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    return _mongoCollection.Find(filter, options);




}

public IFindFluent<TDocument, TDocument> Find<TDocument>(FilterDefinition<TDocument> filter, FindOptions options = null)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    return _mongoCollection.Find(filter, options);
}

/// <summary>
/// Inserts multiple documents of type <typeparamref name="TDocument"/> into the MongoDB collection.
/// </summary>
/// <typeparam name="TDocument">The type of the documents to insert.</typeparam>
/// <param name="documents">The collection of documents to insert.</param>
/// <param name="options">Options for the insert operation (optional).</param>
/// <param name="cancellationToken">Cancellation token (optional).</param>
/// <returns>A task representing the asynchronous insert operation.</returns>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="documents"/> is null.</exception>
public Task InsertManyAsync<TDocument>(IEnumerable<TDocument> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
{
    // Get the MongoDB collection for the specified document type.
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    // Insert the documents into the MongoDB collection asynchronously.
    return _mongoCollection.InsertManyAsync(documents, options, cancellationToken);
}

/// <summary>
/// Inserts a single document of type <typeparamref name="TDocument"/> into the MongoDB collection.
/// </summary>
/// <typeparam name="TDocument">The type of the document to insert.</typeparam>
/// <param name="document">The document to insert.</param>
/// <param name="options">Options for the insert operation (optional).</param>
/// <param name="cancellationToken">Cancellation token (optional).</param>
/// <returns>A task representing the asynchronous insert operation.</returns>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="document"/> is null.</exception>
public async Task InsertOneAsync<TDocument>(TDocument document, InsertOneOptions options = null, CancellationToken cancellationToken = default) where TDocument : class
{
    // Get the MongoDB collection for the specified document type.
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    // Insert the document into the MongoDB collection asynchronously.
    await _mongoCollection.InsertOneAsync(document, options, cancellationToken);
}

public async Task<UpdateResult> UpdateManyAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    return await _mongoCollection.UpdateManyAsync(filter, update, options, cancellationToken);
}

public async Task<UpdateResult> UpdateManyAsync<TDocument>(IClientSessionHandle session, FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    return await _mongoCollection.UpdateManyAsync(session, filter, update, options, cancellationToken);

}

public async Task<UpdateResult> UpdateOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();
    return await _mongoCollection.UpdateOneAsync(filter, update, options, cancellationToken);
}



public async Task<UpdateResult> UpdateOneAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
{
    var _mongoCollection = _collectionService.GetCollectionName<TDocument>();


    return await _mongoCollection.UpdateOneAsync(filter, update, options, cancellationToken);


}
}

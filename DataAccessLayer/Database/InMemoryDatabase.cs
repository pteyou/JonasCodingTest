using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Database
{
	public class InMemoryDatabase<T> : IDbWrapper<T> where T : DataEntity
	{
		private Dictionary<Tuple<string, string>, DataEntity> DatabaseInstance;
		private readonly ILogger _logger;

		public InMemoryDatabase(ILogger logger)
		{
			DatabaseInstance = new Dictionary<Tuple<string, string>, DataEntity>();
			_logger = logger;
            _logger.LogInformation("Database creation");
        }

        public bool Insert(T data)
		{
			try
			{
				DatabaseInstance.Add(Tuple.Create(data.SiteId, data.CompanyCode), data);
				_logger.LogInformation($"Inserted data with siteId {data.SiteId} in database");
				return true;
			}
			catch(Exception ex)
			{
                _logger.LogError($"Failed to insert data with siteId {data.SiteId} in database with error : {ex.Message}");
                return false;
			}
		}

		public bool Update(T data)
		{
			try
			{
				if (DatabaseInstance.ContainsKey(Tuple.Create(data.SiteId, data.CompanyCode)))
				{
					DatabaseInstance.Remove(Tuple.Create(data.SiteId, data.CompanyCode));
					Insert(data);
                    _logger.LogInformation($"Updated data with siteId {data.SiteId} in database");
                    return true;
				}
                _logger.LogError($"Failed to update data with siteId {data.SiteId} in database, data not found");
                return false;
			}
			catch (Exception ex)
			{
                _logger.LogError($"Failed to update data with siteId {data.SiteId} in database with error : {ex.Message}");
                return false;
			}
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
		{
			try
			{
				var entities = FindAll();
				return entities.Where(expression.Compile());
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
		}

		public IEnumerable<T> FindAll()
		{
			try
			{
				var result = DatabaseInstance.Values.OfType<T>();
				if(result.Any())
				{
                    _logger.LogInformation($"Found data set in database");
                }
				else
				{
                    _logger.LogInformation($"No data found in database");
                }
                return result;
			}
			catch (Exception ex)
			{
                _logger.LogError($"Error in searching data in database : {ex.Message}");
                return Enumerable.Empty<T>();
			}
		}

		public bool Delete(Expression<Func<T, bool>> expression)
		{
			try
			{
				var entities = FindAll();
				var entity = entities.Where(expression.Compile()).ToList();
				foreach (var dataEntity in entity)
				{
					DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
				}
				_logger.LogInformation($"Removed {entity.Count} data entities from database");
				return true;
			}
			catch (Exception ex)
			{
                _logger.LogError($"Error in deleting data from database : {ex.Message}");
                return false;
			}
		}

		public bool DeleteAll()
		{
			try
			{
				DatabaseInstance.Clear();
				_logger.LogInformation($"Erased all the in memory database for the data type ${typeof(T)}");
				return true;
			}
			catch (Exception ex)
			{
                _logger.LogError($"Error in deleting all data from database : {ex.Message}");
                return false;
			}
		}

		public bool UpdateAll(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
		{
			try
			{
				var entities = FindAll();
				var entity = entities.Where(filter.Compile());
				foreach (var dataEntity in entity)
				{
					var newEntity = UpdateProperty(dataEntity, fieldToUpdate, newValue);

					DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
					Insert(newEntity);
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		private T UpdateProperty(T dataEntity, string key, object value)
		{
			Type t = typeof(T);
			var prop = t.GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (prop == null)
			{
				throw new Exception("Property not found");
			}

			prop.SetValue(dataEntity, value, null);
			return dataEntity;
		}

		public Task<bool> InsertAsync(T data)
		{
			return Task.FromResult(Insert(data));
		}

		public Task<bool> UpdateAsync(T data)
		{
			return Task.FromResult(Update(data));
		}

		public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
		{
			return Task.FromResult(Find(expression));
		}

		public Task<IEnumerable<T>> FindAllAsync()
		{
			return Task.FromResult(FindAll());
		}

		public Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
		{
			return Task.FromResult(Delete(expression));
		}

		public Task<bool> DeleteAllAsync()
		{
			return Task.FromResult(DeleteAll());
		}

		public Task<bool> UpdateAllAsync(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
		{
			return Task.FromResult(UpdateAll(filter, fieldToUpdate, newValue));
		}

	
	}
}

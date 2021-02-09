using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class, IEntity, new()
    {
       

        /// <summary>
        /// Veri kaynağından aldığımız tüm entity listeler
        /// </summary>
        /// <returns> listesi döndürür.</returns>
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        /// <summary>  // todo: sonradan bak.
        /// Veri kaynağından, entity'i şarta göre getirir.
        /// </summary>
        /// <param name=""></param>
        /// <returns>Bir tek bir entity döndürür.</returns>
        T Get(Expression<Func<T, bool>> filter);
        /// <summary>
        /// id'ye göre Entity getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetByID(int id);


        // todo: ne işe yaradığını yazmayı unutma
        object Select(Expression<Func<T, bool>> exp);

        bool Any(Expression<Func<T, bool>> exp);

        T FirstOrDefault(Expression<Func<T, bool>> exp);


        /// <summary>
        /// Veri kaynağına entity ekler
        /// </summary>
        /// <param name="entity">Eklenecek entity</param>
        void Add(T entity);

        /// <summary>
        /// Veri kaynağınadaki entity'i Günceller.
        /// </summary>
        /// <param name="entity">Güncel Entity bilgileri</param>
        void Update(T entity);

        /// <summary>
        /// Veri kaynağınan entityi Siler
        /// </summary>
        /// <param name="entity">Silinecek entity</param>
        void Delete(T entity);
    }
}

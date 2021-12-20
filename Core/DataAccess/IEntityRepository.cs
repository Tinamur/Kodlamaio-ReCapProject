using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //Genereic Constraint T ye gelen değerleri sınırla
    //class demek referans tip olabilir demek
    //IEntity ise I entity veya implementleri olabilir demek
    //new ise newlene bilir demek I Entity newlenemediği için gelemiyor
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        //Expression GetAll ı çağırdığında GetAll(p=>p. falan filan)
        //tarzında GetAll içinde Expressionlar yazmanı sağlar
        //Filter= null filtreleme işlemin null olsada çalış demek
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

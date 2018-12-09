using System;
using System.Collections.Generic;
using System.Text;

namespace FunApp.Data.Common
{
    public abstract class BaseModel<T>
    {
        public T Id { get; set; }
    }
}

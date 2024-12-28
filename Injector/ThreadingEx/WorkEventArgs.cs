#region Usings

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace ThreadingEx
{
    public class WorkEventArgs
    {
        public readonly IWork Work;
        private Type resultType;
        private object result;

        public WorkEventArgs(IWork work)
        {
            Work = work;
        }

        public WorkState State
        {
            get { return Work.State; }
        }

        public Exception Exception
        {
            get { return Work.Exception; }
        }

        public void SetResult<T>(T result)
        {
            resultType = typeof(T);
            this.result = result;
        }

        public T GetResult<T>()
        {
            Type t = typeof(T);
            if (t == resultType)
            {
                return (T)result;
            }
            return default(T);
        }
    }

}
